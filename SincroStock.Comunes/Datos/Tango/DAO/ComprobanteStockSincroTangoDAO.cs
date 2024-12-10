using GC.Utils;
using SincroStock.Comunes.Datos.Tango.DTO;
using SincroStock.Comunes.Negocio;
using SincroStock.Comunes.Utils;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GC.Utils.Helpers;
using GC.Utils.SQL;
using GC.Tango.Common.Datos.DTO;
using System.Runtime.CompilerServices;

namespace SincroStock.Comunes.Datos.Tango.DAO
{
    public static class ComprobanteStockSincroTangoDAO
    {
        private static ILog logger = LogManager.GetLogger(typeof(ComprobanteStockSincroTangoDAO));

        public static List<SincroMovimientoStockOrigenDTO> GetMovimientosPendientesOrigen()
        {
            LogUtil.Log(logger, Level.Debug, $"Ingresando a {nameof(GetMovimientosPendientesOrigen)}()");

            var movimientosDTODictio = new Dictionary<(string TCOMP_IN_S, string NCOMP_IN_S), SincroMovimientoStockOrigenDTO>();
            SincroMovimientoStockOrigenDTO sincroDTO;
            ComprobanteStockTangoDTO comprobanteStockDTO;
            ComprobanteStockTangoItemDTO itemComprobanteStockDTO;


            ConfigGeneral cfg = ConfigGeneral.Instance;
            (string TCOMP_IN_S, string NCOMP_IN_S) identificadorMov;
            using (var sqlConn = GC.Utils.SQL.SqlDAO.AbrirConexion(ConfigGeneral.Instance.TangoDBConnectionStringOrigen))
            using (var sqlCmd = new SqlCommand() { Connection = sqlConn })
            {
                sqlCmd.CommandTimeout = ConfigGeneral.Instance.TimeoutSqlCommandEnSegundosOrigen;
                //sqlCmd.CommandText = "EXEC SP_HC_SINCRO_STOCK_MOVIMIENTOS_PENDIENTES @ANTIGUEDAD_MAX_DIAS";
                sqlCmd.CommandText = "EXEC SP_HC_SINCRO_STOCK_MOVIMIENTOS_PENDIENTES @MODO_EJECUCION;";
                sqlCmd.Parameters.AddWithValueNullable("@MODO_EJECUCION", "S");

                using (var sqlReader = sqlCmd.ExecuteReaderCustom(cfg.TangoDBCommandRetries, cfg.TangoDBCommandSecondsBetweenRetries, true))
                {
                    while (sqlReader.Read())
                    {
                        identificadorMov = (TCOMP_IN_S: DataConversion.GetValueFromSQL<string>(sqlReader, "ORIG_TCOMP_IN_S"),
                                            NCOMP_IN_S: DataConversion.GetValueFromSQL<string>(sqlReader, "ORIG_NCOMP_IN_S"));
                        sincroDTO = GetSincroMovimientoOrigenDTO(sqlReader);
                        movimientosDTODictio.Add(identificadorMov, sincroDTO);
                    }
                    sqlReader.NextResult();
                    while (sqlReader.Read())
                    {
                        identificadorMov = (TCOMP_IN_S: DataConversion.GetValueFromSQL<string>(sqlReader, "TCOMP_IN_S"),
                                            NCOMP_IN_S: DataConversion.GetValueFromSQL<string>(sqlReader, "NCOMP_IN_S"));
                        comprobanteStockDTO = GetComprobanteStockTangoDTO(sqlReader);
                        movimientosDTODictio[identificadorMov].ComprobanteStock = comprobanteStockDTO;
                    }
                    sqlReader.NextResult();
                    while (sqlReader.Read())
                    {
                        identificadorMov = (TCOMP_IN_S: DataConversion.GetValueFromSQL<string>(sqlReader, "TCOMP_IN_S"),
                                            NCOMP_IN_S: DataConversion.GetValueFromSQL<string>(sqlReader, "NCOMP_IN_S"));
                        itemComprobanteStockDTO = GetComprobanteStockTangoItemDTO(sqlReader);
                        movimientosDTODictio[identificadorMov].ComprobanteStock.Items.Add(itemComprobanteStockDTO);
                    }
                }
            }


            return movimientosDTODictio.Values.ToList();
        }

        public static int GetCountMovimientosErrorOrigen()
        {
            LogUtil.Log(logger, Level.Debug, $"Ingresando a {nameof(GetCountMovimientosErrorOrigen)}()");

            ConfigGeneral cfg = ConfigGeneral.Instance;

            using (var sqlConn = GC.Utils.SQL.SqlDAO.AbrirConexion(ConfigGeneral.Instance.TangoDBConnectionStringOrigen))
            using (var sqlCmd = new SqlCommand() { Connection = sqlConn })
            {
                //sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = ConfigGeneral.Instance.TimeoutSqlCommandEnSegundosOrigen;
                sqlCmd.CommandText = "EXEC SP_HC_SINCRO_STOCK_MOVIMIENTOS_PENDIENTES @MODO_EJECUCION;";
                sqlCmd.Parameters.AddWithValueNullable("@MODO_EJECUCION", "C");

                return DataConversion.GetValueFromSQL<int>(sqlCmd.ExecuteScalarCustom(cfg.TangoDBCommandRetries, cfg.TangoDBCommandSecondsBetweenRetries, true));
            }
        }
        internal static SincroMovimientoStockOrigenDTO GetSincroMovimientoOrigenDTO(SqlDataReader sqlReader)
        {
            return new SincroMovimientoStockOrigenDTO()
            {
                ID_HC_SINCRO_STOCK_MOVIMIENTO_ORIGEN = DataConversion.GetValueFromSQL<int?>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_ORIGEN)),
                ORIG_ID_STA14 = DataConversion.GetValueFromSQL<int>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.ORIG_ID_STA14)),
                ORIG_TCOMP_IN_S = DataConversion.GetValueFromSQL<EnumTipoComprobanteStockTango>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.ORIG_TCOMP_IN_S)),
                ORIG_NCOMP_IN_S = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.ORIG_NCOMP_IN_S)),
                ORIG_T_COMP = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.ORIG_T_COMP)),
                ORIG_N_COMP = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.ORIG_N_COMP)),
                ORIG_COD_PRO_CL = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.ORIG_COD_PRO_CL)),
                ORIG_FECHA_MOV = DataConversion.GetValueFromSQL<DateTime>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.ORIG_FECHA_MOV)),
                ORIG_ESTADO_MOV = DataConversion.GetValueFromSQL<EnumEstadoMovimientoStockTango?>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.ORIG_ESTADO_MOV), defaultValue: null, raiseConversionException: false),
                ESTADO_SINCRO = DataConversion.GetValueFromSQL<EnumEstadoSincroStock>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.ESTADO_SINCRO)),
                DETALLE_ULTIMA_SINCRO = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.DETALLE_ULTIMA_SINCRO)),
                FECHA_ULTIMA_SINCRO = DataConversion.GetValueFromSQL<DateTime?>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.FECHA_ULTIMA_SINCRO)),
                CANT_INTENTOS = DataConversion.GetValueFromSQL<int>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.CANT_INTENTOS), defaultValue: 0, raiseConversionException: false),
                DEST_T_COMP = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.DEST_T_COMP)),
                DEST_N_COMP = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.DEST_N_COMP)),
                DEST_TCOMP_IN_S = DataConversion.GetValueFromSQL<EnumTipoComprobanteStockTango?>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.DEST_TCOMP_IN_S)),
                DEST_NCOMP_IN_S = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.DEST_NCOMP_IN_S)),
                DEST_ANU_T_COMP = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.DEST_ANU_T_COMP)),
                DEST_ANU_N_COMP = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.DEST_ANU_N_COMP)),
                DEST_ANU_TCOMP_IN_S = DataConversion.GetValueFromSQL<EnumTipoComprobanteStockTango?>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.DEST_ANU_TCOMP_IN_S)),
                DEST_ANU_NCOMP_IN_S = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.DEST_ANU_NCOMP_IN_S)),
                OMISION_EXTERNA = DataConversion.GetValueFromSQL<bool>(sqlReader, nameof(SincroMovimientoStockOrigenDTO.OMISION_EXTERNA)),
            };
        }

        internal static ComprobanteStockTangoDTO GetComprobanteStockTangoDTO(SqlDataReader sqlReader)
        {
            return new ComprobanteStockTangoDTO()
            {
                ID_STA14 = DataConversion.GetValueFromSQL<int>(sqlReader, nameof(ComprobanteStockTangoDTO.ID_STA14)),
                TCOMP_IN_S = DataConversion.GetValueFromSQL<EnumTipoComprobanteStockTango>(sqlReader, nameof(ComprobanteStockTangoDTO.TCOMP_IN_S)),
                NCOMP_IN_S = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(ComprobanteStockTangoDTO.NCOMP_IN_S)),
                FECHA_MOV = DataConversion.GetValueFromSQL<DateTime>(sqlReader, nameof(ComprobanteStockTangoDTO.FECHA_MOV)),
                T_COMP = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(ComprobanteStockTangoDTO.T_COMP)),
                N_COMP = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(ComprobanteStockTangoDTO.N_COMP)),
                COD_PRO_CL = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(ComprobanteStockTangoDTO.COD_PRO_CL)),
                ESTADO_MOV = DataConversion.GetValueFromSQL<EnumEstadoMovimientoStockTango?>(sqlReader, nameof(ComprobanteStockTangoDTO.ESTADO_MOV), defaultValue: null, raiseConversionException: false),
                FECHA_ANU = DataConversion.GetValueFromSQL<DateTime?>(sqlReader, nameof(ComprobanteStockTangoDTO.FECHA_ANU)),

            };
        }

        internal static ComprobanteStockTangoItemDTO GetComprobanteStockTangoItemDTO(SqlDataReader sqlReader)
        {
            return new ComprobanteStockTangoItemDTO()
            {
                ID_STA20 = DataConversion.GetValueFromSQL<int>(sqlReader, nameof(ComprobanteStockTangoItemDTO.ID_STA20)),
                N_RENGL_S = DataConversion.GetValueFromSQL<int>(sqlReader, nameof(ComprobanteStockTangoItemDTO.N_RENGL_S)),
                COD_ARTICU = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(ComprobanteStockTangoItemDTO.COD_ARTICU)),
                USA_PARTID = DataConversion.GetValueFromSQL<int>(sqlReader, nameof(ComprobanteStockTangoItemDTO.USA_PARTID)) == 1,
                COD_DEPOSI = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(ComprobanteStockTangoItemDTO.COD_DEPOSI)),
                TIPO_MOV = DataConversion.GetValueFromSQL<EnumTipoMovStockTango>(sqlReader, nameof(ComprobanteStockTangoItemDTO.TIPO_MOV)),
                CANTIDAD = DataConversion.GetValueFromSQL<decimal>(sqlReader, nameof(ComprobanteStockTangoItemDTO.CANTIDAD)),
                COD_DEPOSI_DESTINO = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(ComprobanteStockTangoItemDTO.COD_DEPOSI_DESTINO)),

            };
        }

        internal static ComprobanteStockTangoItemPartidaDTO GetComprobanteStockTangoItemPartidaDTO(SqlDataReader sqlReader)
        {
            return new ComprobanteStockTangoItemPartidaDTO()
            {
                ID_STA09 = DataConversion.GetValueFromSQL<int>(sqlReader, nameof(ComprobanteStockTangoItemPartidaDTO.ID_STA09)),
                N_PARTIDA = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(ComprobanteStockTangoItemPartidaDTO.N_PARTIDA)),
                CANTIDAD = DataConversion.GetValueFromSQL<decimal>(sqlReader, nameof(ComprobanteStockTangoItemPartidaDTO.CANTIDAD)),

            };
        }

        internal static SincroMovimientoStockDestinoDTO GetSincroMovimientoStockDestinoDTO(SqlDataReader sqlReader)
        {
            var sincroDTO = new SincroMovimientoStockDestinoDTO()
            {
                ID_HC_SINCRO_STOCK_MOVIMIENTO_DESTINO = DataConversion.GetValueFromSQL<int>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_DESTINO)),
                ORIG_ID_STA14 = DataConversion.GetValueFromSQL<int>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.ORIG_ID_STA14)),
                ORIG_TCOMP_IN_S = DataConversion.GetValueFromSQL<EnumTipoComprobanteStockTango>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.ORIG_TCOMP_IN_S)),
                ORIG_NCOMP_IN_S = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.ORIG_NCOMP_IN_S)),
                ORIG_FECHA_MOV = DataConversion.GetValueFromSQL<DateTime>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.ORIG_FECHA_MOV)),
                ORIG_T_COMP = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.ORIG_T_COMP)),
                ORIG_N_COMP = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.ORIG_N_COMP)),
                ORIG_COD_PRO_CL = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.ORIG_COD_PRO_CL)),
                FECHA_ULTIMA_SINCRO = DataConversion.GetValueFromSQL<DateTime>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.FECHA_ULTIMA_SINCRO)),
                DEST_TCOMP_IN_S = DataConversion.GetValueFromSQL<EnumTipoComprobanteStockTango>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.DEST_TCOMP_IN_S)),
                DEST_NCOMP_IN_S = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.DEST_NCOMP_IN_S)),
                DEST_ANU_TCOMP_IN_S = DataConversion.GetValueFromSQL<EnumTipoComprobanteStockTango?>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.DEST_ANU_TCOMP_IN_S)),
                DEST_ANU_NCOMP_IN_S = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.DEST_ANU_NCOMP_IN_S)),
                DEST_T_COMP = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.DEST_T_COMP)),
                DEST_N_COMP = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.DEST_N_COMP)),
                DEST_ANU_T_COMP = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.DEST_ANU_T_COMP)),
                DEST_ANU_N_COMP = DataConversion.GetValueFromSQL<string>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.DEST_ANU_N_COMP))            };
            if (sqlReader.GetColumnsNames().Contains(nameof(SincroMovimientoStockDestinoDTO.DEST_STA14_ESTADO_MOV)))
                sincroDTO.DEST_STA14_ESTADO_MOV = DataConversion.GetValueFromSQL<EnumEstadoMovimientoStockTango?>(sqlReader, nameof(SincroMovimientoStockDestinoDTO.DEST_STA14_ESTADO_MOV), defaultValue: null, raiseConversionException: false);

            return sincroDTO;
        }

        public static void GrabarSincroMovimientoStockOrigenDTO(SincroMovimientoStockOrigenDTO sincroDTO)
        {
            LogUtil.Log(logger, Level.Debug, $"Ingresando a {nameof(GrabarSincroMovimientoStockOrigenDTO)}()");

            ConfigGeneral cfg = ConfigGeneral.Instance;
            using (var sqlConn = GC.Utils.SQL.SqlDAO.AbrirConexion(ConfigGeneral.Instance.TangoDBConnectionStringOrigen))
            using (SqlTransaction sqlTran = sqlConn.BeginTransaction())
            using (var sqlCmd = new SqlCommand() { Connection = sqlConn, Transaction = sqlTran })
            {
                //sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = ConfigGeneral.Instance.TimeoutSqlCommandEnSegundosOrigen;
                sqlCmd.CommandText =
                    !sincroDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_ORIGEN.HasValue ?
                    @"
INSERT INTO HC_SINCRO_STOCK_MOVIMIENTO_ORIGEN(ORIG_ID_STA14, ORIG_FECHA_MOV,  ORIG_TCOMP_IN_S, ORIG_NCOMP_IN_S, ORIG_T_COMP, ORIG_N_COMP, ORIG_COD_PRO_CL, ORIG_ESTADO_MOV, 
                                              ESTADO_SINCRO, DETALLE_ULTIMA_SINCRO, FECHA_ULTIMA_SINCRO, CANT_INTENTOS,
                                              DEST_TCOMP_IN_S, DEST_NCOMP_IN_S, DEST_T_COMP, DEST_N_COMP, 
                                              DEST_ANU_TCOMP_IN_S, DEST_ANU_NCOMP_IN_S, DEST_ANU_T_COMP, DEST_ANU_N_COMP)
VALUES(@ORIG_ID_STA14, @ORIG_FECHA_MOV, @ORIG_TCOMP_IN_S, @ORIG_NCOMP_IN_S, @ORIG_T_COMP, @ORIG_N_COMP, @ORIG_COD_PRO_CL, @ORIG_ESTADO_MOV, 
       @ESTADO_SINCRO, @DETALLE_ULTIMA_SINCRO, @FECHA_ULTIMA_SINCRO, @CANT_INTENTOS,
       @DEST_TCOMP_IN_S, @DEST_NCOMP_IN_S, @DEST_T_COMP, @DEST_N_COMP,  
       @DEST_ANU_TCOMP_IN_S, @DEST_ANU_NCOMP_IN_S, @DEST_ANU_T_COMP, @DEST_ANU_N_COMP)

SELECT SCOPE_IDENTITY() AS ID_HC_SINCRO_STOCK_MOVIMIENTO_ORIGEN;
"
:
@"UPDATE HC_SINCRO_STOCK_MOVIMIENTO_ORIGEN SET 
    ORIG_ID_STA14 = @ORIG_ID_STA14, 
    ORIG_FECHA_MOV = @ORIG_FECHA_MOV, 
    ORIG_TCOMP_IN_S = @ORIG_TCOMP_IN_S, 
    ORIG_NCOMP_IN_S = @ORIG_NCOMP_IN_S, 
    ORIG_T_COMP = @ORIG_T_COMP, 
    ORIG_N_COMP = @ORIG_N_COMP, 
    ORIG_COD_PRO_CL = @ORIG_COD_PRO_CL, 
    ORIG_ESTADO_MOV = @ORIG_ESTADO_MOV, 
    ESTADO_SINCRO = @ESTADO_SINCRO, 
    DETALLE_ULTIMA_SINCRO = @DETALLE_ULTIMA_SINCRO, 
    FECHA_ULTIMA_SINCRO = @FECHA_ULTIMA_SINCRO, 
    CANT_INTENTOS = @CANT_INTENTOS,
    DEST_T_COMP = @DEST_T_COMP, 
    DEST_N_COMP = @DEST_N_COMP, 
    DEST_TCOMP_IN_S = @DEST_TCOMP_IN_S, 
    DEST_NCOMP_IN_S = @DEST_NCOMP_IN_S, 
    DEST_ANU_T_COMP = @DEST_ANU_T_COMP, 
    DEST_ANU_N_COMP = @DEST_ANU_N_COMP, 
    DEST_ANU_TCOMP_IN_S = @DEST_ANU_TCOMP_IN_S, 
    DEST_ANU_NCOMP_IN_S = @DEST_ANU_NCOMP_IN_S
WHERE ID_HC_SINCRO_STOCK_MOVIMIENTO_ORIGEN = @ID_HC_SINCRO_STOCK_MOVIMIENTO_ORIGEN;
";
                ;

                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_ORIGEN), sincroDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_ORIGEN);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_ID_STA14), sincroDTO.ORIG_ID_STA14);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_FECHA_MOV), sincroDTO.ORIG_FECHA_MOV);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_TCOMP_IN_S), sincroDTO.ORIG_TCOMP_IN_S.GetEnumValueName());
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_NCOMP_IN_S), sincroDTO.ORIG_NCOMP_IN_S);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_T_COMP), sincroDTO.ORIG_T_COMP);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_N_COMP), sincroDTO.ORIG_N_COMP);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_COD_PRO_CL), sincroDTO.ORIG_COD_PRO_CL);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_ESTADO_MOV), sincroDTO.ORIG_ESTADO_MOV?.GetEnumValueName() ?? "");
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ESTADO_SINCRO), sincroDTO.ESTADO_SINCRO.GetEnumValueName());
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.DETALLE_ULTIMA_SINCRO), sincroDTO.DETALLE_ULTIMA_SINCRO);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.FECHA_ULTIMA_SINCRO), sincroDTO.FECHA_ULTIMA_SINCRO);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.CANT_INTENTOS), sincroDTO.CANT_INTENTOS);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.DEST_T_COMP), sincroDTO.DEST_T_COMP);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.DEST_N_COMP), sincroDTO.DEST_N_COMP);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.DEST_TCOMP_IN_S), sincroDTO.DEST_TCOMP_IN_S?.GetEnumValueName());
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.DEST_NCOMP_IN_S), sincroDTO.DEST_NCOMP_IN_S);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.DEST_ANU_T_COMP), sincroDTO.DEST_ANU_T_COMP);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.DEST_ANU_N_COMP), sincroDTO.DEST_ANU_N_COMP);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.DEST_ANU_TCOMP_IN_S), sincroDTO.DEST_ANU_TCOMP_IN_S?.GetEnumValueName());
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.DEST_ANU_NCOMP_IN_S), sincroDTO.DEST_ANU_NCOMP_IN_S);

                if (!sincroDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_ORIGEN.HasValue)
                    sincroDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_ORIGEN = DataConversion.GetValueFromSQL<int>(sqlCmd.ExecuteScalarCustom(cfg.TangoDBCommandRetries, cfg.TangoDBCommandSecondsBetweenRetries, true), nameof(sincroDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_ORIGEN));
                else
                    sqlCmd.ExecuteNonQueryCustom(cfg.TangoDBCommandRetries, cfg.TangoDBCommandSecondsBetweenRetries, true);

                sqlTran.Commit();

            }

        }

        public static ComprobanteStockTangoDTO GetComprobanteStockTangoDestinoDTO(EnumTipoComprobanteStockTango TCOMP_IN_S, string NCOMP_IN_S)
        {
            LogUtil.Log(logger, Level.Debug, $"Ingresando a {nameof(GetComprobanteStockTangoDestinoDTO)}()");

            ComprobanteStockTangoDTO comprobanteStockDTO;
            ComprobanteStockTangoItemDTO itemComprobanteStockDTO;
            ComprobanteStockTangoItemPartidaDTO partidaItemComprobanteStockDTO; 

            ConfigGeneral cfg = ConfigGeneral.Instance;
            int idSTA20;
            using (var sqlConn = GC.Utils.SQL.SqlDAO.AbrirConexion(ConfigGeneral.Instance.TangoDBConnectionStringDestino))
            using (var sqlCmd = new SqlCommand() { Connection = sqlConn })
            {
                //sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = ConfigGeneral.Instance.TimeoutSqlCommandEnSegundosDestino;
                sqlCmd.CommandText = @"
SELECT 
STA14.ID_STA14,
STA14.FECHA_MOV,
STA14.TCOMP_IN_S,
STA14.NCOMP_IN_S,
STA14.T_COMP,
STA14.N_COMP,
STA14.COD_PRO_CL,
STA14.ESTADO_MOV,
CASE WHEN ISNULL(STA14.FECHA_ANU,'18000101') = '18000101' THEN NULL ELSE STA14.FECHA_ANU END AS FECHA_ANU
FROM STA14
WHERE 
STA14.TCOMP_IN_S = @TCOMP_IN_S 
AND STA14.NCOMP_IN_S = @NCOMP_IN_S
AND EXISTS (SELECT TOP 1 1 
              FROM STA20
              INNER JOIN STA11 ON STA20.COD_ARTICU = STA11.COD_ARTICU
              WHERE STA14.TCOMP_IN_S = STA20.TCOMP_IN_S AND STA14.NCOMP_IN_S = STA20.NCOMP_IN_S
              AND STA11.PROMO_MENU <> 'P'
              AND STA11.STOCK = 1)
ORDER BY STA14.ID_STA14;

SELECT 
STA14.ID_STA14,
STA20.ID_STA20,
STA20.N_RENGL_S,
STA20.COD_ARTICU,
STA11.USA_PARTID,
STA20.COD_DEPOSI,
STA20.TIPO_MOV,
STA20.CANTIDAD,
'' AS COD_DEPOSI_DESTINO
FROM STA14
INNER JOIN STA20 ON STA14.TCOMP_IN_S = STA20.TCOMP_IN_S AND STA14.NCOMP_IN_S = STA20.NCOMP_IN_S
INNER JOIN STA11 ON STA20.COD_ARTICU = STA11.COD_ARTICU
WHERE 
STA14.TCOMP_IN_S = @TCOMP_IN_S
AND STA14.NCOMP_IN_S = @NCOMP_IN_S 
AND STA11.PROMO_MENU <> 'P'
AND STA11.STOCK = 1
ORDER BY STA14.ID_STA14, STA20.ID_STA20;

SELECT 
STA20.ID_STA20,
STA09.ID_STA09,
STA09.N_PARTIDA,
STA09.CANTIDAD
FROM STA14
INNER JOIN STA20 ON STA14.TCOMP_IN_S = STA20.TCOMP_IN_S AND STA14.NCOMP_IN_S = STA20.NCOMP_IN_S
INNER JOIN STA11 ON STA20.COD_ARTICU = STA11.COD_ARTICU
INNER JOIN STA09 ON STA20.TCOMP_IN_S = STA09.TCOMP_IN_S AND STA20.NCOMP_IN_S = STA09.NCOMP_IN_S AND STA20.N_RENGL_S = STA09.N_RENGL_S
WHERE 
STA14.TCOMP_IN_S = @TCOMP_IN_S
AND STA14.NCOMP_IN_S = @NCOMP_IN_S 
AND STA11.PROMO_MENU <> 'P'
AND STA11.STOCK = 1
AND STA11.USA_PARTID = 1
ORDER BY STA14.ID_STA14, STA20.ID_STA20, STA09.ID_STA09;

";
                sqlCmd.Parameters.AddWithValueNullable(nameof(TCOMP_IN_S), TCOMP_IN_S.GetEnumValueName());
                sqlCmd.Parameters.AddWithValueNullable(nameof(NCOMP_IN_S), NCOMP_IN_S);
                
                using (var sqlReader = sqlCmd.ExecuteReaderCustom(cfg.TangoDBCommandRetries, cfg.TangoDBCommandSecondsBetweenRetries, true))
                {
                    sqlReader.Read();
                    comprobanteStockDTO = GetComprobanteStockTangoDTO(sqlReader);
                    sqlReader.NextResult();
                    while (sqlReader.Read())
                    {
                        itemComprobanteStockDTO = GetComprobanteStockTangoItemDTO(sqlReader);
                        comprobanteStockDTO.Items.Add(itemComprobanteStockDTO);
                    }
                    sqlReader.NextResult();
                    while (sqlReader.Read())
                    {
                        idSTA20 = DataConversion.GetValueFromSQL<int>(sqlReader, nameof(itemComprobanteStockDTO.ID_STA20));
                        partidaItemComprobanteStockDTO = GetComprobanteStockTangoItemPartidaDTO(sqlReader);
                        comprobanteStockDTO.Items.Single(i => i.ID_STA20 == idSTA20).Partidas.Add(partidaItemComprobanteStockDTO);
                    }
                }
            }


            return comprobanteStockDTO;
        }

        public static void GrabarSincroMovimientoStockDestinoDTO(SincroMovimientoStockDestinoDTO sincroDTO, SqlTransaction sqlTran)
        {
            LogUtil.Log(logger, Level.Debug, $"Ingresando a {nameof(GrabarSincroMovimientoStockDestinoDTO)}()");

            ConfigGeneral cfg = ConfigGeneral.Instance;
            //using (var sqlConn = GC.Utils.SQL.SqlDAO.AbrirConexion(ConfigGeneral.Instance.TangoDBConnectionStringDestino))
            //using (SqlTransaction sqlTran = sqlConn.BeginTransaction())
            using (var sqlCmd = new SqlCommand() { Connection = sqlTran.Connection, Transaction = sqlTran })
            {
                //sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = ConfigGeneral.Instance.TimeoutSqlCommandEnSegundosDestino;
                sqlCmd.CommandText =
                    !sincroDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_DESTINO.HasValue ?
                    @"
INSERT INTO HC_SINCRO_STOCK_MOVIMIENTO_DESTINO(ORIG_ID_STA14, ORIG_TCOMP_IN_S, ORIG_NCOMP_IN_S, ORIG_FECHA_MOV, ORIG_T_COMP, ORIG_N_COMP, 
                                              ORIG_COD_PRO_CL, DEST_TCOMP_IN_S, DEST_NCOMP_IN_S, DEST_ANU_TCOMP_IN_S, DEST_ANU_NCOMP_IN_S, FECHA_ULTIMA_SINCRO)
VALUES(@ORIG_ID_STA14, @ORIG_TCOMP_IN_S, @ORIG_NCOMP_IN_S, @ORIG_FECHA_MOV, @ORIG_T_COMP, @ORIG_N_COMP, 
                                              @ORIG_COD_PRO_CL, @DEST_TCOMP_IN_S, @DEST_NCOMP_IN_S, @DEST_ANU_TCOMP_IN_S, @DEST_ANU_NCOMP_IN_S, @FECHA_ULTIMA_SINCRO);"
:
@"UPDATE HC_SINCRO_STOCK_MOVIMIENTO_DESTINO SET 
    ORIG_ID_STA14 = @ORIG_ID_STA14,
    ORIG_TCOMP_IN_S = @ORIG_TCOMP_IN_S, 
    ORIG_NCOMP_IN_S = @ORIG_NCOMP_IN_S, 
    ORIG_FECHA_MOV = @ORIG_FECHA_MOV, 
    ORIG_T_COMP = @ORIG_T_COMP, 
    ORIG_N_COMP = @ORIG_N_COMP, 
    ORIG_COD_PRO_CL = @ORIG_COD_PRO_CL,
    DEST_TCOMP_IN_S = @DEST_TCOMP_IN_S, 
    DEST_NCOMP_IN_S = @DEST_NCOMP_IN_S, 
    DEST_ANU_TCOMP_IN_S = @DEST_ANU_TCOMP_IN_S, 
    DEST_ANU_NCOMP_IN_S = @DEST_ANU_NCOMP_IN_S,
    FECHA_ULTIMA_SINCRO = @FECHA_ULTIMA_SINCRO
WHERE ID_HC_SINCRO_STOCK_MOVIMIENTO_DESTINO = @ID_HC_SINCRO_STOCK_MOVIMIENTO_DESTINO;
";
                ;

                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_DESTINO), sincroDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_DESTINO);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_ID_STA14), sincroDTO.ORIG_ID_STA14);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_TCOMP_IN_S), sincroDTO.ORIG_TCOMP_IN_S.GetEnumValueName());
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_NCOMP_IN_S), sincroDTO.ORIG_NCOMP_IN_S);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_T_COMP), sincroDTO.ORIG_T_COMP);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_N_COMP), sincroDTO.ORIG_N_COMP);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_FECHA_MOV), sincroDTO.ORIG_FECHA_MOV);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.ORIG_COD_PRO_CL), sincroDTO.ORIG_COD_PRO_CL);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.DEST_TCOMP_IN_S), sincroDTO.DEST_TCOMP_IN_S.GetEnumValueName());
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.DEST_NCOMP_IN_S), sincroDTO.DEST_NCOMP_IN_S);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.DEST_ANU_TCOMP_IN_S), sincroDTO.DEST_ANU_TCOMP_IN_S?.GetEnumValueName());
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.DEST_ANU_NCOMP_IN_S), sincroDTO.DEST_ANU_NCOMP_IN_S);
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDTO.FECHA_ULTIMA_SINCRO), sincroDTO.FECHA_ULTIMA_SINCRO);

                if (!sincroDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_DESTINO.HasValue)
                    sincroDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_DESTINO = DataConversion.GetValueFromSQL<int>(sqlCmd.ExecuteScalarCustom(cfg.TangoDBCommandRetries, cfg.TangoDBCommandSecondsBetweenRetries, true), nameof(sincroDTO.ID_HC_SINCRO_STOCK_MOVIMIENTO_DESTINO));
                else
                    sqlCmd.ExecuteNonQueryCustom(cfg.TangoDBCommandRetries, cfg.TangoDBCommandSecondsBetweenRetries, true);

                //sqlTran.Commit();

            }

        }
        //public static void GrabarSincroStockMovimientoDestinoDTO(SincroStockMovimientoDestinoDTO sincroDTO)
        //{
        //    ConfigGeneral cfg = ConfigGeneral.Instance;
        //    using (var sqlConn = GC.Utils.SQL.SqlDAO.AbrirConexion(ConfigGeneral.Instance.TangoDBConnectionStringDestino))
        //    using (SqlTransaction sqlTran = sqlConn.BeginTransaction())
        //    {
        //        GrabarSincroStockMovimientoDestinoDTO(sincroDTO, sqlTran);
        //        sqlTran.Commit();
        //    }

        //}

        public static SincroMovimientoStockDestinoDTO GetSincroMovimientoStockDestinoDTO(int ORIG_ID_STA14)
        {
            LogUtil.Log(logger, Level.Debug, $"Ingresando a {nameof(GetSincroMovimientoStockDestinoDTO)}()");

            SincroMovimientoStockDestinoDTO sincroDTO;

            ConfigGeneral cfg = ConfigGeneral.Instance;

            using (var sqlConn = GC.Utils.SQL.SqlDAO.AbrirConexion(ConfigGeneral.Instance.TangoDBConnectionStringDestino))
            using (var sqlCmd = new SqlCommand() { Connection = sqlConn })
            {
                //sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = ConfigGeneral.Instance.TimeoutSqlCommandEnSegundosDestino;
                sqlCmd.CommandText = @"
SELECT SINCRO.*, 
STA14.ESTADO_MOV AS DEST_STA14_ESTADO_MOV, STA14.T_COMP AS DEST_T_COMP, STA14.N_COMP AS DEST_N_COMP, STA14.TCOMP_IN_S AS DEST_TCOMP_IN_S, STA14.NCOMP_IN_S AS DEST_NCOMP_IN_S,
STA14_ANU.T_COMP AS DEST_ANU_T_COMP, STA14_ANU.N_COMP AS DEST_ANU_N_COMP, STA14_ANU.TCOMP_IN_S AS DEST_ANU_TCOMP_IN_S, STA14_ANU.NCOMP_IN_S AS DEST_ANU_NCOMP_IN_S
FROM HC_SINCRO_STOCK_MOVIMIENTO_DESTINO AS SINCRO
LEFT JOIN STA14 ON SINCRO.DEST_TCOMP_IN_S = STA14.TCOMP_IN_S AND SINCRO.DEST_NCOMP_IN_S = STA14.NCOMP_IN_S
LEFT JOIN STA14 AS STA14_ANU ON SINCRO.DEST_ANU_TCOMP_IN_S = STA14_ANU.TCOMP_IN_S AND SINCRO.DEST_ANU_NCOMP_IN_S = STA14_ANU.NCOMP_IN_S
WHERE 
SINCRO.ORIG_ID_STA14 = @ORIG_ID_STA14;

";
                sqlCmd.Parameters.AddWithValueNullable(nameof(ORIG_ID_STA14), ORIG_ID_STA14);

                using (var sqlReader = sqlCmd.ExecuteReaderCustom(cfg.TangoDBCommandRetries, cfg.TangoDBCommandSecondsBetweenRetries, true))
                {
                    return !sqlReader.Read() ? null : sincroDTO = GetSincroMovimientoStockDestinoDTO(sqlReader);
                }

            }


            return sincroDTO;
        }


        public static void GrabarAjusteStockTangoDestino(GC.Tango.Stock.Negocio.AjusteStock ajusteTango, 
            SincroMovimientoStockDestinoDTO sincroDestinoDTO,
            DateTime fechaSincro)
        {
            LogUtil.Log(logger, Level.Debug, $"Ingresando a {nameof(GrabarAjusteStockTangoDestino)}()");

            var cfg = ConfigGeneral.Instance;
            using(var sqlConn = GC.Utils.SQL.SqlDAO.AbrirConexion(cfg.TangoDBConnectionStringDestino))
            using(var sqlTran = sqlConn.BeginTransaction())
            {
                LogUtil.Log(logger, Level.Debug, $"Grabando ajuste en Tango");
                GC.Tango.Stock.Negocio.AjusteStock.Grabar(sqlConn, ajusteTango, sqlTran, cfg.TimeoutSqlCommandEnSegundosDestino);
                //sincroDestinoDTO.DEST_STA14_ESTADO_MOV = null;
                sincroDestinoDTO.FECHA_ULTIMA_SINCRO = fechaSincro;
                sincroDestinoDTO.DEST_TCOMP_IN_S = (EnumTipoComprobanteStockTango) Enum.Parse(typeof(EnumTipoComprobanteStockTango), ajusteTango.TipoComprobanteInterno);
                sincroDestinoDTO.DEST_NCOMP_IN_S = ajusteTango.NumeroInternoNormalizado;
                sincroDestinoDTO.DEST_T_COMP = ajusteTango.TipoComprobante;
                sincroDestinoDTO.DEST_N_COMP = ajusteTango.NumeroComprobanteNormalizado;
                GrabarSincroMovimientoStockDestinoDTO(sincroDestinoDTO, sqlTran);
                sqlTran.Commit();
            }                
        }

        public static bool GrabarAjusteStockTangoAnulacionDestino(GC.Tango.Stock.Negocio.AjusteStock ajusteTango,
            SincroMovimientoStockDestinoDTO sincroDestinoDTO, DateTime fechaSincro)
        {
            LogUtil.Log(logger, Level.Debug, $"Ingresando a {nameof(GrabarAjusteStockTangoAnulacionDestino)}()");

            bool ajusteGenerado;
            var cfg = ConfigGeneral.Instance;
            using (var sqlConn = GC.Utils.SQL.SqlDAO.AbrirConexion(cfg.TangoDBConnectionStringDestino))
            using (var sqlTran = sqlConn.BeginTransaction())
            using (var sqlCmd = new SqlCommand() { Connection = sqlConn, Transaction = sqlTran })
            {
                LogUtil.Log(logger, Level.Debug, $"Verificando estado del movimiento");
                //sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = ConfigGeneral.Instance.TimeoutSqlCommandEnSegundosDestino;
                sqlCmd.CommandText = "SELECT ESTADO_MOV FROM STA14 WHERE TCOMP_IN_S = @DEST_TCOMP_IN_S AND STA14.NCOMP_IN_S = @DEST_NCOMP_IN_S;";
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDestinoDTO.DEST_TCOMP_IN_S), sincroDestinoDTO.DEST_TCOMP_IN_S.GetEnumValueName());
                sqlCmd.Parameters.AddWithValueNullable(nameof(sincroDestinoDTO.DEST_NCOMP_IN_S), sincroDestinoDTO.DEST_NCOMP_IN_S);

                var estadoTango = DataConversion.GetValueFromSQL<EnumEstadoMovimientoStockTango?>(sqlCmd.ExecuteScalarCustom(cfg.TangoDBCommandRetries, cfg.TangoDBCommandSecondsBetweenRetries, true), 
                    null, false, "STA14.ESTADO_MOV");

                LogUtil.Log(logger, Level.Debug, $"Anulado: {(estadoTango == EnumEstadoMovimientoStockTango.A ? "SI" : "NO")}");
                sincroDestinoDTO.FECHA_ULTIMA_SINCRO = fechaSincro;
                if (estadoTango == EnumEstadoMovimientoStockTango.A)
                {
                    //sincroDestinoDTO.DEST_STA14_ESTADO_MOV = EnumEstadoMovimientoStockTango.A;
                    ajusteGenerado = false;
                }
                else
                {
                    LogUtil.Log(logger, Level.Debug, $"Grabando ajuste por anulación en Tango");
                    GC.Tango.Stock.Negocio.AjusteStock.Grabar(sqlConn, ajusteTango, sqlTran, cfg.TimeoutSqlCommandEnSegundosDestino);
                    sincroDestinoDTO.DEST_ANU_TCOMP_IN_S = (EnumTipoComprobanteStockTango)Enum.Parse(typeof(EnumTipoComprobanteStockTango), ajusteTango.TipoComprobanteInterno);
                    sincroDestinoDTO.DEST_ANU_NCOMP_IN_S = ajusteTango.NumeroInternoNormalizado;
                    sincroDestinoDTO.DEST_ANU_T_COMP = ajusteTango.TipoComprobante;
                    sincroDestinoDTO.DEST_ANU_N_COMP = ajusteTango.NumeroComprobanteNormalizado;
                    GrabarSincroMovimientoStockDestinoDTO(sincroDestinoDTO, sqlTran);
                    ajusteGenerado = true;
                }

                sqlTran.Commit();

                return ajusteGenerado;


            }
        }
    }
}
