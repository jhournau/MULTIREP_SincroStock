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
    public class SincronizadorStock
    {
        private static ILog logger = LogManager.GetLogger(typeof(SincronizadorStock));
        private List<SincroStockMovimiento> sincroStockMovimientos;

        public int PrepararMovimientos(CancellationToken cancellationToken)
        {
            LogUtil.LogWithGui(logger, Level.Info, $"Preparando movimientos a sincronizar", false);

            VerificarCancellationToken(cancellationToken);
            var movPendientes = ComprobanteStockSincroTangoDAO.GetMovimientosPendientesOrigen();
            VerificarCancellationToken(cancellationToken);
            
            if (movPendientes.Count != 0)
                sincroStockMovimientos = movPendientes.ConvertAll(c => new SincroStockMovimiento(c));

            return (sincroStockMovimientos?.Count ?? 0);
        }

        public void SincronizarMovimientos(CancellationToken cancellationToken, Action<SincroStockMovimiento> actionPostSincro)
        {
            VerificarCancellationToken(cancellationToken);

            if ((this.sincroStockMovimientos?.Count ?? 0) == 0)
                throw new Exception("No existen movimientos preparados para sincronizar");

            try
            {
                foreach (var sincroMovimiento in this.sincroStockMovimientos)
                {
                    LogUtil.LogWithGui(logger, Level.Info, $"Sincronizando | {sincroMovimiento.DescripcionSincro}", true);

                    try
                    {
                        VerificarCancellationToken(cancellationToken);
                        sincroMovimiento.Sincronizar();
                        LogUtil.LogWithGui(logger, Level.Info, $"Resultado: Completado | Estado Sincro: {sincroMovimiento.SincroMovimientoOrigenDTO.ESTADO_SINCRO.GetDescription()} | Detalle: {sincroMovimiento.SincroMovimientoOrigenDTO.DETALLE_ULTIMA_SINCRO}", true);
                    }
                    catch (Exception ex)
                    {

                        Exception innerEx = ex is AggregateException ? ex.InnerException : ex;
                        if (innerEx is SincroException)
                        {
                            LogUtil.LogWithGui(logger, Level.Error, $"Resultado: Error | {innerEx.Message}", true, innerEx);
                            continue;
                        }
                        else
                            throw;
                    }
                    actionPostSincro?.Invoke(sincroMovimiento);
                }
            }
            finally
            {
                this.sincroStockMovimientos = null;
            }

        }

        private void VerificarCancellationToken(CancellationToken cancellationToken)
        {
            if (cancellationToken != null && cancellationToken.IsCancellationRequested)
                throw new UserAbortException();
        }

        public void NotificarErroneos(CancellationToken cancellationToken)
        {
            var cfg = ConfigGeneral.Instance;
            LogUtil.LogWithGui(logger, Level.Debug, $"Analizando movimientos erróneos para notificación (fecha última notificación: {cfg.FechaUltimaNotificacionEmail.Value.ToString("dd/MM/yyyy HH:mm:ss")})", false);

            var now = DateTime.Now;

            if (!cfg.AlertaViaMailErroresActivada)
            {
                LogUtil.LogWithGui(logger, Level.Debug, $"Finalizado. Notificación vía mail desactivada. ", false);
            }
            else if (now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday
                || (now.Hour < 9 || now.Hour > 18))
            {
                LogUtil.LogWithGui(logger, Level.Debug, $"Finalizado. Horario actual fuera del rango de notificación (envía 9 a 18)", false);
            }
            else if ((now - cfg.FechaUltimaNotificacionEmail.Value).TotalMinutes < cfg.AlertasFlushInterval)
            {
                LogUtil.LogWithGui(logger, Level.Debug, $"Finalizado. Han pasado {Convert.ToInt64((now - cfg.FechaUltimaNotificacionEmail.Value).TotalMinutes)} minutos desde la última notificación. Frecuencia máxima configurada {cfg.AlertasFlushInterval} minutos.", false);
            }
            else
            {
                VerificarCancellationToken(cancellationToken);
                int cantidadErroneos = ComprobanteStockSincroTangoDAO.GetCountMovimientosErrorOrigen();
                if (cantidadErroneos == 0)
                {
                    LogUtil.LogWithGui(logger, Level.Debug, $"Finalizado. No se detectaron movimientos con error", false);
                }
                else
                {
                    VerificarCancellationToken(cancellationToken);
                    cfg.FechaUltimaNotificacionEmail = cfg.FechaUltimaNotificacionEmail;
                    LogUtil.LogWithGui(logger, Level.Debug, $"Notificando...Detectados {cantidadErroneos} movimientos con error", false);
                    UtilsIFC.EnviarMailNotificacion(cfg.AsuntoEmail, 
                        "Existe movimientos de stock con error de sincronización. Por favor, verificar reporte.",
                        cfg.DestinatariosMailAlertas);
                    cfg.FechaUltimaNotificacionEmail = now;
                }
            }

        }

    }
}
