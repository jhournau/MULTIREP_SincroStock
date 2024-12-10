using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GC.Utils.Helpers;
using SincroStock.Comunes.Datos.Tango.DAO;
using log4net;
using log4net.Core;
using SincroStock.Comunes.Utils;
using System.Threading;
using System.Text.RegularExpressions;
using SincroStock.Comunes.Datos.Tango.DTO;
using System.Windows.Forms;
using GC.Tango.Stock.Negocio;
using System.Net;
using System.Security.Policy;
using GC.Tango.Stock.Datos.DTO;
using SincroStock.Comunes.Exceptions;

namespace SincroStock.Comunes.Negocio
{
    public class SincroStockMovimiento
    {
        private static ILog logger = LogManager.GetLogger(typeof(SincroStockMovimiento));

        public SincroMovimientoStockOrigenDTO SincroMovimientoOrigenDTO { get; private set; }
        //public EnumEstadoSincroStock EstadoSincro { get; private set; }

        public string DescripcionSincro 
        { 
            get 
            {
                string detalleSincro = "";
                detalleSincro += "N° int: ";
                detalleSincro += this.SincroMovimientoOrigenDTO.ORIG_TCOMP_IN_S + " ";
                detalleSincro += this.SincroMovimientoOrigenDTO.ORIG_NCOMP_IN_S + " | ";
                detalleSincro += "N°: ";
                detalleSincro += this.SincroMovimientoOrigenDTO.ORIG_T_COMP + " ";
                detalleSincro += this.SincroMovimientoOrigenDTO.ORIG_N_COMP;
                detalleSincro += String.IsNullOrEmpty(this.SincroMovimientoOrigenDTO.ORIG_COD_PRO_CL) ? "" : " " + this.SincroMovimientoOrigenDTO.ORIG_COD_PRO_CL;
                detalleSincro += " | Estado Mov: " + (this.SincroMovimientoOrigenDTO.ORIG_ESTADO_MOV?.GetDescription() ?? "");
                detalleSincro += " | Fecha Mov " + this.SincroMovimientoOrigenDTO.ORIG_FECHA_MOV.ToString("dd/MM/yyyy");
                detalleSincro += " | Fecha Anu: " + (this.SincroMovimientoOrigenDTO.ComprobanteStock.FECHA_ANU.HasValue ? this.SincroMovimientoOrigenDTO.ComprobanteStock.FECHA_ANU.Value.ToString("dd/MM/yyyy") : "");
                detalleSincro += " | Estado Sincro: " + SincroMovimientoOrigenDTO.ESTADO_SINCRO.GetDescription();
                detalleSincro += " | Detalle última sincro: " + (SincroMovimientoOrigenDTO.DETALLE_ULTIMA_SINCRO ?? "");

                return detalleSincro;
            }
        }

        public SincroStockMovimiento(SincroMovimientoStockOrigenDTO sincroMovimientoOrigenDTO)
        {
            if (sincroMovimientoOrigenDTO == null)
                throw new ArgumentNullException(nameof(sincroMovimientoOrigenDTO), $"El parámetro \"{sincroMovimientoOrigenDTO}\" no puede ser null");
            
            this.SincroMovimientoOrigenDTO = sincroMovimientoOrigenDTO;
            //this.EstadoSincro = this.SincroMovimientoOrigenDTO.ESTADO;
        }

        public void Sincronizar()
        {
            bool generarAjuste;
            bool generarAnulacionAjuste;

            this.GetAccionesSincro(out generarAjuste, out generarAnulacionAjuste);

            //if (!this.SincroMovimientoOrigenDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_ORIGEN.HasValue)
            //    ComprobanteStockSincroTangoDAO.GrabarSincroMovimientoStockOrigenDTO(this.SincroMovimientoOrigenDTO);

            if (generarAjuste)
                this.SincronizarAjuste();
            else if (generarAnulacionAjuste)
                this.SincronizarAnulacionAjuste();

        }

        private void SincronizarAjuste()
        {
            try
            {
                LogUtil.Log(logger, Level.Debug, $"Ingresando a {nameof(SincronizarAjuste)}()");

                
                bool asignarIdentificadorComprobanteDesdeDestino = false;
                SincroMovimientoStockDestinoDTO sincroDestinoDTO;

                this.SincroMovimientoOrigenDTO.FECHA_ULTIMA_SINCRO = DateTime.Now;
                this.SincroMovimientoOrigenDTO.CANT_INTENTOS = (this.SincroMovimientoOrigenDTO.CANT_INTENTOS ?? 0) + 1;

                sincroDestinoDTO = ComprobanteStockSincroTangoDAO.GetSincroMovimientoStockDestinoDTO(this.SincroMovimientoOrigenDTO.ORIG_ID_STA14);

                //Se verifica si ya está en destino (puede haberse generado ajuste y auditoría en destino pero no haberse grabado auditorí en origen
                //Las auditorías no son transaccionales
                if (sincroDestinoDTO != null)
                {
                    this.SincroMovimientoOrigenDTO.ESTADO_SINCRO = EnumEstadoSincroStock.FIN;
                    this.SincroMovimientoOrigenDTO.DETALLE_ULTIMA_SINCRO = "OK (verificación extra)";
                    asignarIdentificadorComprobanteDesdeDestino = true;
                }
                else
                {
                    if (SincroMovimientoOrigenDTO.OMISION_EXTERNA)
                    {
                        this.SincroMovimientoOrigenDTO.ESTADO_SINCRO = EnumEstadoSincroStock.OMI;
                        this.SincroMovimientoOrigenDTO.DETALLE_ULTIMA_SINCRO = "Omitido externamente";
                    }
                    else
                    {

                        var ajusteTango = GetAjusteStockTango();
                        if (ajusteTango.Renglones.Count == 0)
                        {
                            this.SincroMovimientoOrigenDTO.ESTADO_SINCRO = EnumEstadoSincroStock.OMI;
                            this.SincroMovimientoOrigenDTO.DETALLE_ULTIMA_SINCRO = "Sin ítems calculados para sincronizar";
                        }
                        else
                        {
                            sincroDestinoDTO = new SincroMovimientoStockDestinoDTO()
                            {
                                ORIG_COD_PRO_CL = this.SincroMovimientoOrigenDTO.ORIG_COD_PRO_CL,
                                ORIG_FECHA_MOV = this.SincroMovimientoOrigenDTO.ORIG_FECHA_MOV,
                                ORIG_ID_STA14 = this.SincroMovimientoOrigenDTO.ORIG_ID_STA14,
                                ORIG_NCOMP_IN_S = this.SincroMovimientoOrigenDTO.ORIG_NCOMP_IN_S,
                                ORIG_TCOMP_IN_S = this.SincroMovimientoOrigenDTO.ORIG_TCOMP_IN_S,
                                ORIG_T_COMP = this.SincroMovimientoOrigenDTO.ORIG_T_COMP,
                                ORIG_N_COMP = this.SincroMovimientoOrigenDTO.ORIG_N_COMP
                            };
                            ComprobanteStockSincroTangoDAO.GrabarAjusteStockTangoDestino(ajusteTango, sincroDestinoDTO,
                            this.SincroMovimientoOrigenDTO.FECHA_ULTIMA_SINCRO.Value);
                            this.SincroMovimientoOrigenDTO.ESTADO_SINCRO = EnumEstadoSincroStock.FIN;
                            this.SincroMovimientoOrigenDTO.DETALLE_ULTIMA_SINCRO = "OK";
                            this.SincroMovimientoOrigenDTO.FECHA_ULTIMA_SINCRO = sincroDestinoDTO.FECHA_ULTIMA_SINCRO;
                            asignarIdentificadorComprobanteDesdeDestino = true;
                        }
                    }
                }

                if (asignarIdentificadorComprobanteDesdeDestino)
                {
                    this.SincroMovimientoOrigenDTO.DEST_T_COMP = sincroDestinoDTO.DEST_T_COMP;
                    this.SincroMovimientoOrigenDTO.DEST_N_COMP = sincroDestinoDTO.DEST_N_COMP;
                    this.SincroMovimientoOrigenDTO.DEST_TCOMP_IN_S = sincroDestinoDTO.DEST_TCOMP_IN_S;
                    this.SincroMovimientoOrigenDTO.DEST_NCOMP_IN_S = sincroDestinoDTO.DEST_NCOMP_IN_S;
                }
                ComprobanteStockSincroTangoDAO.GrabarSincroMovimientoStockOrigenDTO(this.SincroMovimientoOrigenDTO);
            }
            catch (Exception ex)
            {
                this.SincroMovimientoOrigenDTO.ESTADO_SINCRO = EnumEstadoSincroStock.ERR;
                this.SincroMovimientoOrigenDTO.DETALLE_ULTIMA_SINCRO = ex.Message;
                ComprobanteStockSincroTangoDAO.GrabarSincroMovimientoStockOrigenDTO(this.SincroMovimientoOrigenDTO);
                throw new AggregateException(new SincroException(ex.Message, ex));

            }
        }
        private void SincronizarAnulacionAjuste()
        {
            try
            {
                LogUtil.Log(logger, Level.Debug, $"Ingresando a {nameof(SincronizarAnulacionAjuste)}()");


                bool ajusteAnulacionGenerado = false;
                bool asignarIdentificadorComprobanteDesdeDestino = false;
                SincroMovimientoStockDestinoDTO sincroDestinoDTO;

                this.SincroMovimientoOrigenDTO.FECHA_ULTIMA_SINCRO = DateTime.Now;
                this.SincroMovimientoOrigenDTO.CANT_INTENTOS = (this.SincroMovimientoOrigenDTO.CANT_INTENTOS ?? 0) + 1;

                sincroDestinoDTO = ComprobanteStockSincroTangoDAO.GetSincroMovimientoStockDestinoDTO(this.SincroMovimientoOrigenDTO.ORIG_ID_STA14);

                //Se verifica si ya está en destino (puede haberse generado ajuste y auditoría en destino pero no haberse grabado auditorí en origen
                //Las auditorías no son transaccionales
                if (sincroDestinoDTO.DEST_ANU_TCOMP_IN_S.HasValue)
                {
                    this.SincroMovimientoOrigenDTO.ESTADO_SINCRO = EnumEstadoSincroStock.AFI;
                    this.SincroMovimientoOrigenDTO.DETALLE_ULTIMA_SINCRO = "OK (verificación extra)";
                    asignarIdentificadorComprobanteDesdeDestino = true;
                }
                else if (SincroMovimientoOrigenDTO.OMISION_EXTERNA)
                {
                    this.SincroMovimientoOrigenDTO.ESTADO_SINCRO = EnumEstadoSincroStock.AOM; ;
                    this.SincroMovimientoOrigenDTO.DETALLE_ULTIMA_SINCRO = "Omitido externamente";
                }
                else
                {
                    var comprobanteStockDestino = ComprobanteStockSincroTangoDAO.GetComprobanteStockTangoDestinoDTO(
                        this.SincroMovimientoOrigenDTO.DEST_TCOMP_IN_S.Value,
                        this.SincroMovimientoOrigenDTO.DEST_NCOMP_IN_S);

                    var ajusteTango = GetAjusteAnulacionStockTango(comprobanteStockDestino);
                    ajusteAnulacionGenerado = ComprobanteStockSincroTangoDAO.GrabarAjusteStockTangoAnulacionDestino(ajusteTango, sincroDestinoDTO,
                        this.SincroMovimientoOrigenDTO.FECHA_ULTIMA_SINCRO.Value);

                    if (!ajusteAnulacionGenerado)
                    {
                        this.SincroMovimientoOrigenDTO.ESTADO_SINCRO = EnumEstadoSincroStock.AOM;
                        this.SincroMovimientoOrigenDTO.DETALLE_ULTIMA_SINCRO = "El movimiento original ya se encuentra anulado en destino";
                    }
                    else
                    {
                        this.SincroMovimientoOrigenDTO.ESTADO_SINCRO = EnumEstadoSincroStock.AFI;
                        this.SincroMovimientoOrigenDTO.DETALLE_ULTIMA_SINCRO = "OK";
                        asignarIdentificadorComprobanteDesdeDestino = true;
                    }
                }

                if(asignarIdentificadorComprobanteDesdeDestino)
                {
                    this.SincroMovimientoOrigenDTO.DEST_ANU_T_COMP = sincroDestinoDTO.DEST_ANU_T_COMP;
                    this.SincroMovimientoOrigenDTO.DEST_ANU_N_COMP = sincroDestinoDTO.DEST_ANU_N_COMP;
                    this.SincroMovimientoOrigenDTO.DEST_ANU_TCOMP_IN_S = sincroDestinoDTO.DEST_ANU_TCOMP_IN_S;
                    this.SincroMovimientoOrigenDTO.DEST_ANU_NCOMP_IN_S = sincroDestinoDTO.DEST_ANU_NCOMP_IN_S;
                }

                ComprobanteStockSincroTangoDAO.GrabarSincroMovimientoStockOrigenDTO(this.SincroMovimientoOrigenDTO);
            }
            catch (Exception ex)
            {
                this.SincroMovimientoOrigenDTO.ESTADO_SINCRO = EnumEstadoSincroStock.AER;
                this.SincroMovimientoOrigenDTO.DETALLE_ULTIMA_SINCRO = ex.Message;
                ComprobanteStockSincroTangoDAO.GrabarSincroMovimientoStockOrigenDTO(this.SincroMovimientoOrigenDTO); 
                throw new AggregateException(new SincroException(ex.Message, ex));
            }
        }

        private void GetAccionesSincro(out bool generarAjuste, out bool generarAnulacionAjuste)
        {
            LogUtil.Log(logger, Level.Debug, "Evaluando acciones de sincronización a realizar");

            generarAjuste = false;
            generarAnulacionAjuste = false;

            switch (this.SincroMovimientoOrigenDTO.ESTADO_SINCRO)
            {
                case EnumEstadoSincroStock.PEN:
                    generarAjuste = true; break;
                case EnumEstadoSincroStock.ERR:
                    if (this.SincroMovimientoOrigenDTO.ComprobanteStock.ESTADO_MOV == EnumEstadoMovimientoStockTango.A)
                        generarAnulacionAjuste = true;
                    else
                        generarAjuste = true;
                    break;
                case EnumEstadoSincroStock.AER:
                case EnumEstadoSincroStock.APE:
                    generarAnulacionAjuste = true; break;
                case EnumEstadoSincroStock.FIN:
                    if (this.SincroMovimientoOrigenDTO.ComprobanteStock.ESTADO_MOV == EnumEstadoMovimientoStockTango.A)
                        generarAnulacionAjuste = true;
                    break;
                default:
                    throw new Exception($"Un movimiento de stock en estado {this.SincroMovimientoOrigenDTO.ESTADO_SINCRO.GetDescription()} no puede ser sincronizado");
            };

            LogUtil.Log(logger, Level.Debug, $"Genera ajuste: {(generarAjuste ? "SI" : "NO")} | Genera ajuste por anulación: {(generarAnulacionAjuste ? "SI" : "NO")}");

        }

        private AjusteStock GetAjusteStockTango()
        {

            ConfigGeneral cfg = ConfigGeneral.Instance;
            decimal cantidad;

            var ajuste = new AjusteStock()
            {
                AsignarPartidasAutomaticamente = true,
                OrdenarPartidasPorNumeracion = false,
                IgnorarDescargaDeStockEnNegativo = false,
                IgnorarInhabilitacionArticulo = true,
                UsarPartidaDefaultEnCasoDeNoDetectarPartidasAutomaticamente = false,
                FechaEmision = this.SincroMovimientoOrigenDTO.ComprobanteStock.FECHA_MOV,
                Leyenda1 = $"{this.SincroMovimientoOrigenDTO.ComprobanteStock.TCOMP_IN_S.GetEnumValueName()} {this.SincroMovimientoOrigenDTO.ComprobanteStock.NCOMP_IN_S}",
                Leyenda2 = $"{this.SincroMovimientoOrigenDTO.ComprobanteStock.T_COMP} {this.SincroMovimientoOrigenDTO.ComprobanteStock.N_COMP} {this.SincroMovimientoOrigenDTO.ComprobanteStock.COD_PRO_CL}",
                TerminalIngreso = Dns.GetHostName()?.ToUpper() ?? "",
                UsuarioIngreso = "IFC",
                TipoComprobante = cfg.TipoComprobanteAjusteStock,
                PermitirArticulosConSeriesSiNoEsObligatorio = true
            };

            foreach(var gItem in this.SincroMovimientoOrigenDTO.ComprobanteStock.Items.GroupBy(i => (i.COD_DEPOSI_DESTINO, i.COD_ARTICU)))
            {
                cantidad = gItem.Sum(i => i.CANTIDAD * (i.TIPO_MOV == EnumTipoMovStockTango.S ? -1 : 1));
                if (cantidad == 0)
                    break;
                ajuste.Renglones.Add(new RenglonMovimientoStockAjuste()
                {
                    CodArticulo = gItem.Key.COD_ARTICU,
                    CodDeposito = gItem.Key.COD_DEPOSI_DESTINO,
                    TipoMovimientoRenglon = Convert.ToChar(cantidad < 0 ? EnumTipoMovStockTango.S.GetEnumValueName() : EnumTipoMovStockTango.E.GetEnumValueName()),
                    Cantidad = Math.Abs(cantidad)
                });
            }

            return ajuste;
        }

        private AjusteStock GetAjusteAnulacionStockTango(ComprobanteStockTangoDTO comprobanteTangoDTO)
        {
            ConfigGeneral cfg = ConfigGeneral.Instance;
            RenglonMovimientoStockAjuste rengAjuste;

            var ajuste = new AjusteStock()
            {
                AsignarPartidasAutomaticamente = false,
                OrdenarPartidasPorNumeracion = false,
                IgnorarDescargaDeStockEnNegativo = false,
                IgnorarInhabilitacionArticulo = true,
                UsarPartidaDefaultEnCasoDeNoDetectarPartidasAutomaticamente = false,
                FechaEmision = this.SincroMovimientoOrigenDTO.ComprobanteStock.FECHA_ANU.Value,
                Leyenda1 = $"{this.SincroMovimientoOrigenDTO.ComprobanteStock.TCOMP_IN_S.GetEnumValueName()} {this.SincroMovimientoOrigenDTO.ComprobanteStock.NCOMP_IN_S}",
                Leyenda2 = $"{this.SincroMovimientoOrigenDTO.ComprobanteStock.T_COMP} {this.SincroMovimientoOrigenDTO.ComprobanteStock.N_COMP} {this.SincroMovimientoOrigenDTO.ComprobanteStock.COD_PRO_CL}",
                Leyenda3 = "ANULACION EN ORIGEN",
                TerminalIngreso = Dns.GetHostName()?.ToUpper() ?? "",
                UsuarioIngreso = "IFC",
                TipoComprobante = cfg.TipoComprobanteAjusteStock,
                PermitirArticulosConSeriesSiNoEsObligatorio = true
            };

            foreach (var item in comprobanteTangoDTO.Items)
            {
                rengAjuste = new RenglonMovimientoStockAjuste()
                {
                    CodArticulo = item.COD_ARTICU,
                    CodDeposito = item.COD_DEPOSI,
                    TipoMovimientoRenglon = Convert.ToChar(item.TIPO_MOV == EnumTipoMovStockTango.E ?
                        EnumTipoMovStockTango.S.GetEnumValueName() : EnumTipoMovStockTango.E.GetEnumValueName()),
                    Cantidad = item.CANTIDAD
                };

                ajuste.Renglones.Add(rengAjuste);
                
                foreach(var itemPartida in item.Partidas)
                {
                    rengAjuste.RenglonesPartidas.Add(new RenglonPartida()
                    {
                        Cantidad = itemPartida.CANTIDAD,
                        Partida = new Partida() { NumeroPartida = itemPartida.N_PARTIDA }
                    });
                }
            }

            return ajuste;
        }

    }
}
