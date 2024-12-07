using GC.Utils.Helpers;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;
using System.Globalization;
using SincroStock.Comunes.Negocio;
using log4net;
using SincroStock.Comunes.Utils;
using System.Text;
using log4net.Repository.Hierarchy;

namespace SincroStock.Comunes.Datos
{
    public static class SqlDAO
    {
        private static ILog logger = LogManager.GetLogger(typeof(SqlDAO));

        public static SqlConnection AbrirConexion(string connectionString, int connectionRetries = 2,
    int secondsBetweenConnectionRetries = 1)
        {
            SqlConnectionStringBuilder connStrBuilder = new SqlConnectionStringBuilder(connectionString);
            LogUtil.Log(logger, Level.Debug, $"Abriendo y probando conexión SQL a \"{connStrBuilder.DataSource}\". DatabaseName: {connStrBuilder.InitialCatalog}");
            int connectionTimeout = connStrBuilder.ConnectTimeout;
            SqlConnection conn = null;
            try
            {
                return ExecHelper.Execute<SqlConnection>(
                action: () =>
                {
                    try
                    {
                        conn = new SqlConnection(connStrBuilder.ConnectionString);
                        conn.Open();
                        ValidarConexionAbierta(conn);
                        return conn;
                    }
                    catch
                    {
                        CerrarConexion(conn);
                        throw;
                    }
                }
                , actionTimeoutInMS: connectionTimeout == 0 ? (int?)null : (connectionTimeout > 1 ? connectionTimeout - 1 : connectionTimeout) * 1000
                , retries: connectionRetries
                , delay: TimeSpan.FromSeconds(secondsBetweenConnectionRetries)
                , onRetry: (ex, nroReintento) =>
                {
                    LogUtil.Log(logger, Level.Debug, $"Durmiendo {secondsBetweenConnectionRetries} seg." +
                                            $" y reintentando {nameof(SqlDAO)}.{nameof(AbrirConexion)}() a {connStrBuilder.DataSource} (reintento nro. {nroReintento} de {connectionRetries})...");
                }
                , retryCondition: (ex) =>
                {
                    return EsExcepcionConexion(ex);
                }
                , timeoutException: new SqlTimeoutException($"Se alcanzó el timeout interno de {connectionTimeout} seg. al abrir la conexión SQL a {connStrBuilder.DataSource}"));
            }
            catch (Exception ex)
            {
                LogUtil.Log(logger, Level.Error, $"Error abriendo y probando conexión SQL a \"{connStrBuilder.DataSource}\": {ex.Message}.");
                CerrarConexion(conn);
                if (EsExcepcionConexion(ex))
                    throw new SqlTempException(ex.Message, ex);
                else
                    throw;
            }
        }

        public static T ExecuteCommand<T>(Func<T> action, string commandDetail = "", int commandRetries = 0,
            int secondsBetweenCommandRetries = 1,
            bool loguearCommandDetail = false)
        {
            DateTime inicio = DateTime.Now;
            try
            {
                return ExecHelper.Execute<T>(
                action: () =>
                {

                    T result;
                    if (loguearCommandDetail && !String.IsNullOrEmpty(commandDetail))
                        LogUtil.Log(logger, Level.Debug, $"Ejecutando en SQL...");
                    result = action();
                    if (loguearCommandDetail && !String.IsNullOrEmpty(commandDetail))
                        LogUtil.Log(logger, Level.Debug, $"Fin de ejecución. Tiempo = {Convert.ToInt64((DateTime.Now - inicio).TotalMilliseconds)}ms{Environment.NewLine}{Environment.NewLine}{commandDetail}");
                    return result;

                }
                , retries: commandRetries
                , delay: TimeSpan.FromSeconds(secondsBetweenCommandRetries)
                , onRetry: (ex, nroReintento) => LogUtil.Log(logger, Level.Debug, $"Durmiendo {secondsBetweenCommandRetries} seg." +
                                          $" y reintentando {nameof(SqlDAO)}.{nameof(ExecuteCommand)}() (reintento nro. {nroReintento} de {commandRetries})...")
                , retryCondition: (ex) =>
                {
                    return EsExcepcionTemporal(ex);
                });

            }
            catch (Exception ex)
            {
                string errorMessage = $"Error al ejecutar comandos SQL. Tiempo = {Convert.ToInt64((DateTime.Now - inicio).TotalMilliseconds)}ms{Environment.NewLine}{ex.Message}";
                if (!String.IsNullOrEmpty(commandDetail))
                    errorMessage += $"{Environment.NewLine + Environment.NewLine}Detail:{Environment.NewLine + Environment.NewLine}{commandDetail}";

                LogUtil.Log(logger, Level.Error, errorMessage);

                if (EsExcepcionTemporal(ex))
                    throw new SqlTempException(errorMessage, ex);
                else
                    throw;
            }
        }

        public static void ExecuteCommand(Action action, string commandDetail = "", int commandRetries = 0,
            int secondsBetweenCommandRetries = 1,
            bool loguearCommandDetail = false
            )
        {
            DateTime inicio = DateTime.Now;
            try
            {
                ExecHelper.Execute(
                action: () =>
                {

                    if (loguearCommandDetail && !String.IsNullOrEmpty(commandDetail))
                        LogUtil.Log(logger, Level.Debug, $"Ejecutando en SQL...");
                    action();
                    if (loguearCommandDetail && !String.IsNullOrEmpty(commandDetail))
                        LogUtil.Log(logger, Level.Debug, $"Fin de ejecución. Tiempo = {Convert.ToInt64((DateTime.Now - inicio).TotalMilliseconds)}ms{Environment.NewLine}{Environment.NewLine}{commandDetail}");
                }
                , retries: commandRetries
                , delay: TimeSpan.FromSeconds(secondsBetweenCommandRetries)
                , onRetry: (ex, nroReintento) => LogUtil.Log(logger, Level.Debug, $"Durmiendo {secondsBetweenCommandRetries} seg." +
                                          $" y reintentando {nameof(SqlDAO)}.{nameof(ExecuteCommand)}() (reintento nro. {nroReintento} de {commandRetries})...")
                , retryCondition: (ex) =>
                {
                    return EsExcepcionTemporal(ex);
                });
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error al ejecutar comandos SQL.Tiempo = {Convert.ToInt64((DateTime.Now - inicio).TotalMilliseconds)}ms{Environment.NewLine}{ex.Message}";
                if (!String.IsNullOrEmpty(commandDetail))
                    errorMessage = $"{Environment.NewLine + Environment.NewLine}Detail:{Environment.NewLine + Environment.NewLine}{commandDetail}";

                LogUtil.Log(logger, Level.Error, errorMessage);

                if (EsExcepcionTemporal(ex))
                    throw new SqlTempException(ex.Message, ex);
                else
                    throw;
            }
        }

        public static object ExecuteScalarCustom(this SqlCommand cmd, int commandRetries = 0,
            int secondsBetweenCommandRetries = 1, bool loguearCommandDetail = false)
        {
            return SqlDAO.ExecuteCommand(() => cmd.ExecuteScalar(), cmd.GetCommandDescription(),
                commandRetries, secondsBetweenCommandRetries, loguearCommandDetail);
        }
        public static void ExecuteNonQueryCustom(this SqlCommand cmd, int commandRetries = 0,
            int secondsBetweenCommandRetries = 1, bool loguearCommandDetail = false)
        {
            SqlDAO.ExecuteCommand(() => cmd.ExecuteNonQuery(), cmd.GetCommandDescription(),
                commandRetries, secondsBetweenCommandRetries, loguearCommandDetail);
        }
        public static SqlDataReader ExecuteReaderCustom(this SqlCommand cmd,
            int commandRetries = 0,
            int secondsBetweenCommandRetries = 1, bool loguearCommandDetail = false)
        {
            return SqlDAO.ExecuteCommand(() => cmd.ExecuteReader(), cmd.GetCommandDescription(),
                commandRetries, secondsBetweenCommandRetries, loguearCommandDetail: loguearCommandDetail);
        }

        public static void WriteToServerCustom(
            this SqlBulkCopy sqlCopy,
            SqlConnection sqlConn,
            DataTable dt,
            int commandRetries = 0,
            int secondsBetweenCommandRetries = 1, bool loguearBulkCopyDetail = false)
        {
            SqlDAO.ExecuteCommand(() => sqlCopy.WriteToServer(dt), sqlCopy.GetBulkCopyDescription(sqlConn),
                commandRetries, secondsBetweenCommandRetries, loguearBulkCopyDetail);
        }

        private static void ValidarConexionAbierta(SqlConnection sqlConn)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("SELECT 1", sqlConn))
                    sqlCmd.ExecuteNonQuery();
            }
            catch
            {
                if (sqlConn.State != System.Data.ConnectionState.Open)
                    throw new SqlConnectionException("Se abrió la conexión pero luego de la prueba la misma se cerró");
            }
        }

        public static void CerrarConexion(SqlConnection sqlConn, SqlTransaction sqlTran = null, SqlCommand sqlCmd = null)
        {
            try
            {
                if (sqlConn != null)
                    sqlConn.Close();
            }
            finally
            {
                try
                {
                    if (sqlConn != null)
                        sqlConn.Dispose();
                    if (sqlTran != null)
                        sqlTran.Dispose();
                    if (sqlCmd != null)
                        sqlCmd.Dispose();
                }
                catch
                { }
                finally
                {

                }
            }
        }

        public static bool EsExcepcionConexion(Exception ex)
        {
            if (ex == null)
                throw new ArgumentException($"{nameof(ex)}");
            else if (ex.GetType() == typeof(SqlException))
            {
                List<int> errorCodes = new List<int>() { -2, -1, 2, 53, 4060, 40184, 64, 258, 10054, 1225, 121 };
                return errorCodes.Contains(((SqlException)ex).Number);
            }
            else if (new List<Type>() { typeof(SqlConnectionException), typeof(SqlTimeoutException), typeof(TimeoutException) }.Contains(ex.GetType()))
                return true;

            return false;
        }

        public static bool EsExcepcionTemporal(Exception ex)
        {
            List<int> errorCodes = new List<int>() { 1205, 922 };

            if (ex == null)
                throw new ArgumentException($"{nameof(ex)}");

            return SqlDAO.EsExcepcionConexion(ex)
                || (ex.GetType() == typeof(SqlException) && errorCodes.Contains(((SqlException)ex).Number))
                || (ex.InnerException != null && EsExcepcionTemporal(ex.InnerException));

        }

        public static DateTime GetNowSQLDateTime(string conString)
        {
            SqlConnection sqlCon = null;
            SqlCommand sqlCmd = null;
            try
            {
                sqlCmd = new SqlCommand("SELECT GetDate()");
                sqlCon = AbrirConexion(conString);
                sqlCmd.Connection = sqlCon;

                return Convert.ToDateTime(sqlCmd.ExecuteScalar());
            }
            finally
            {
                CerrarConexion(sqlCon, null, sqlCmd);
            }
        }

        public static string GetScriptDetail(this SqlCommand sqlCommand)
        {
            var scriptBuilder = new StringBuilder();

            foreach (SqlParameter param in sqlCommand.Parameters)
            {
                string sqlType = GetSqlType(param);
                if (param.Direction == ParameterDirection.Input || param.Direction == ParameterDirection.InputOutput)
                {
                    string value = param.GetEnclosedValue();
                    scriptBuilder.AppendLine($"DECLARE {param.ParameterName} {sqlType} = {value};");
                }
                else if (param.Direction == ParameterDirection.Output)
                {
                    scriptBuilder.AppendLine($"DECLARE {param.ParameterName} {sqlType};");
                }
            }
            scriptBuilder.AppendLine();
            scriptBuilder.AppendLine(sqlCommand.CommandText);

            foreach (SqlParameter param in sqlCommand.Parameters)
            {
                if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                {
                    scriptBuilder.AppendLine($"PRINT '{param.ParameterName} = ' + CAST({param.ParameterName} AS NVARCHAR(MAX));");
                }
            }

            return scriptBuilder.ToString();
        }

        // Método auxiliar para determinar el tipo SQL correspondiente
        private static string GetSqlType(SqlParameter param)
        {
            string sqlType = param.SqlDbType.ToString();

            if (param.SqlDbType == SqlDbType.VarChar || param.SqlDbType == SqlDbType.NVarChar || param.SqlDbType == SqlDbType.Char || param.SqlDbType == SqlDbType.NChar)
            {
                sqlType += param.Size > 0 ? $"({param.Size})" : "(MAX)";
            }
            else if (param.SqlDbType == SqlDbType.Decimal)
            {
                sqlType += $"({param.Precision},{param.Scale})";
            }

            return sqlType;
        }

        public static string GetEnclosedValue(this SqlParameter parameter)
        {
            if (parameter.Value == DBNull.Value)
                return "NULL";

            switch (parameter.SqlDbType)
            {
                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.SmallDateTime:
                    return $"'{((DateTime)parameter.Value):yyyy-MM-dd HH:mm:ss.fff}'";

                case SqlDbType.DateTimeOffset:
                    return $"'{((DateTimeOffset)parameter.Value):yyyy-MM-dd HH:mm:ss.fff zzz}'";

                case SqlDbType.UniqueIdentifier:
                    return $"'{parameter.Value}'";

                case SqlDbType.VarChar:
                case SqlDbType.NVarChar:
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                    return $"'{parameter.Value.ToString().Replace("'", "''")}'";

                default:
                    return Convert.ToString(parameter.Value, CultureInfo.InvariantCulture);
            }
        }

        public static string GetCommandDescription(this SqlCommand sqlCommand)
        {
            string cmdDesc = "";

            cmdDesc += $"Instance Name: {sqlCommand.Connection.DataSource}";
            cmdDesc += $"{Environment.NewLine}Database: {sqlCommand.Connection.Database}";
            cmdDesc += $"{Environment.NewLine}Connection Timeout: {sqlCommand.Connection.ConnectionTimeout}";
            cmdDesc += $"{Environment.NewLine}Command Timeout: {sqlCommand.CommandTimeout}";
            cmdDesc += $"{Environment.NewLine}Command Type: {sqlCommand.CommandType}";

            cmdDesc += $"{Environment.NewLine}Command Text:{Environment.NewLine}{Environment.NewLine}{sqlCommand.GetScriptDetail()}";

            return cmdDesc;
        }

        public static string GetBulkCopyDescription(this SqlBulkCopy sqlBulkCopy, SqlConnection conn)
        {
            string desc = "";
            desc += $"Instance Name: {conn.DataSource}";
            desc += $"{Environment.NewLine}Database: {conn.Database}";
            desc += $"{Environment.NewLine}Destination Table Name: {sqlBulkCopy.DestinationTableName}";
            desc += $"{Environment.NewLine}Bulk Copy Timeout: {sqlBulkCopy.BulkCopyTimeout}";

            return desc;
        }
    }

    [Serializable]
    public class SqlTimeoutException : Exception
    {
        public SqlTimeoutException()
        {
        }

        public SqlTimeoutException(string message)
        : base(message)
        {
        }

        public SqlTimeoutException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }

    [Serializable]
    public class SqlTempException : Exception
    {
        public SqlTempException() { }
        public SqlTempException(string message) : base(message) { }
        public SqlTempException(string message, System.Exception inner) : base(message, inner) { }
        protected SqlTempException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class SqlConnectionException : Exception
    {
        public SqlConnectionException()
        {
        }

        public SqlConnectionException(string message)
        : base(message)
        {
        }

        public SqlConnectionException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }

}
