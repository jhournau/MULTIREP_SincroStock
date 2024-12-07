using GC.Utils;
using GC.Utils.EMail;
using GC.Utils.Helpers;
using GC.Utils.Logger;
using log4net;
using SincroStock.Comunes.Negocio;
using SincroStock.Comunes.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SincroStock.Comunes
{
    public class ConfigGeneral : GC.Utils.Configuracion.Config, IDataErrorInfo
    {

        private static ConfigGeneral _instance = null;
        private static Configuration AppConfigFile;
        
        public static ConfigGeneral Instance
        {
            get
            {
                if (ConfigGeneral._instance == null)
                    ConfigGeneral._instance = new ConfigGeneral(ConfigPath);
                return ConfigGeneral._instance;
            }
        }


        protected ConfigGeneral() : base()
        {
        }
        protected ConfigGeneral(string rutaConfig) : base(rutaConfig)
        {
        }

        [XmlIgnore]
        public string GUID { get { return "SINCRO_STOCK_" + "fa872vqd-t542-1614-81ee-9f06bdb8f3e2"; } }
        [XmlIgnore]
        public string NombreApp { get { return "Sincro Tango Stock "; } }
        [XmlIgnore]
        public static string ConfigPath
        {
            get { return Path.Combine(new AssemblyInfo(typeof(ConfigGeneral)).Directory, "Config.xml"); }
        }

        [XmlIgnore]
        public DateTime? FechaUltimaNotificacionEmail
        {
            get
            {
                string strValue = GetAppConfigValue(nameof(FechaUltimaNotificacionEmail));
                return String.IsNullOrEmpty(strValue) ? null : new Nullable<DateTime>(DateTime.Parse(strValue, CultureInfo.InvariantCulture));
            }
            set 
            {
                SetAppConfigValue(nameof(FechaUltimaNotificacionEmail), !value.HasValue ? "" : value.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),  true);
            }
        }


        #region Ejecucion Planificada
        public int PlanificacionPeriodo { get; set; }
        [XmlIgnoreAttribute]
        public decimal PlanificacionPeriodoDec
        {
            get { return Convert.ToDecimal(this.PlanificacionPeriodo); }
            set { this.PlanificacionPeriodo = Convert.ToInt32(value); }
        }
        public UnidadTiempo PlanificacionUnidadTiempo { get; set; }
        [XmlIgnore]
        public int PlanificacionUnidadTiempoInt
        {
            get { return Convert.ToInt32(PlanificacionUnidadTiempo); }
            set { PlanificacionUnidadTiempo = (UnidadTiempo)value; }
        }
        public bool PlanificacionEjecutaLunes { get; set; }
        public bool PlanificacionEjecutaMartes { get; set; }
        public bool PlanificacionEjecutaMiercoles { get; set; }
        public bool PlanificacionEjecutaJueves { get; set; }
        public bool PlanificacionEjecutaViernes { get; set; }
        public bool PlanificacionEjecutaSabado { get; set; }
        public bool PlanificacionEjecutaDomingo { get; set; }
        public DateTime PlanificacionHoraEjecucion { get; set; }
        public DateTime PlanificacionHoraEjecucionDesde { get; set; }
        public DateTime PlanificacionHoraEjecucionHasta { get; set; }
        private bool planificacionEjecutaCadaXUnidades;
        public bool PlanificacionEjecutaCadaXUnidades
        {
            get { return this.planificacionEjecutaCadaXUnidades; }
            set
            {
                this.planificacionEjecutaCadaXUnidades = value;
                this.PlanificacionEjecutaAUnaHoraEspecifica = !value;
            }
        }
        public bool PlanificacionEjecutaAUnaHoraEspecifica { get; set; }
        public bool PlanificacionEjecucionActivada { get; set; }
        [XmlIgnore]
        public string NombreServicio
        {
            get { return "SVC_SincroTangoStock"; }
        }
        [XmlIgnore]
        public string NombreMutexSVC
        {
            get
            {
                return "SVC" + this.GUID;
            }
        }
        
        #endregion Ejecucion Planificada

        #region Conexion a Tango

        public string TangoDBInstanceNameOrigen { get; set; }
        public string TangoDBNameOrigen { get; set; }
        public string TangoDBUserOrigen { get; set; }
        public string TangoDBPasswordOrigen { get; set; }
        public int TimeoutSqlCommandEnSegundosOrigen { get; set; } = 120;
        [XmlIgnore]
        public string TangoDBConnectionStringOrigen
        {
            get
            {
                SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
                connStringBuilder.DataSource = ConfigGeneral.Instance.TangoDBInstanceNameOrigen;
                connStringBuilder.InitialCatalog = ConfigGeneral.Instance.TangoDBNameOrigen;
                connStringBuilder.UserID = ConfigGeneral.Instance.TangoDBUserOrigen;
                connStringBuilder.Password = ConfigGeneral.Instance.TangoDBPasswordOrigen;
                connStringBuilder.MultipleActiveResultSets = true;

                return connStringBuilder.ConnectionString;
            }
        }

        public string TangoDBInstanceNameDestino { get; set; }
        public string TangoDBNameDestino { get; set; }
        public string TangoDBUserDestino { get; set; }
        public string TangoDBPasswordDestino { get; set; }
        public int TimeoutSqlCommandEnSegundosDestino { get; set; } = 120;
        [XmlIgnore]
        public string TangoDBConnectionStringDestino
        {
            get
            {
                SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
                connStringBuilder.DataSource = ConfigGeneral.Instance.TangoDBInstanceNameDestino;
                connStringBuilder.InitialCatalog = ConfigGeneral.Instance.TangoDBNameDestino;
                connStringBuilder.UserID = ConfigGeneral.Instance.TangoDBUserDestino;
                connStringBuilder.Password = ConfigGeneral.Instance.TangoDBPasswordDestino;
                connStringBuilder.MultipleActiveResultSets = true;

                return connStringBuilder.ConnectionString;
            }
        }

        [XmlIgnore]
        public int TangoDBCommandRetries { get { return 3; } }
        public int TangoDBCommandSecondsBetweenRetries { get { return 1; } }
        #endregion Conexion Tango

        #region Registro de actividad

        public bool NivelLogDetallado { get; set; }
        public string ServidorMail { get; set; }
        public string DireccionMail { get; set; }
        public int PuertoMail { get; set; }
        public string UsuarioMail { get; set; }
        public string PasswordMail { get; set; }
        public bool UsaSSL { get; set; }
        public bool AlertaViaMailErroresActivada { get; set; }
        public string DestinatariosMailAlertas { get; set; }
        public int AlertasFlushInterval { get; set; }
        public int MaxLogFileSizeMB { get; set; }
        [XmlIgnore]
        public string RutaDirectorioLog { get { return Path.Combine(new AssemblyInfo(typeof(ConfigGeneral)).Directory, @"Logs"); } }
        [XmlIgnore]
        public string RutaArchivoLogGUI { get { return Path.Combine(this.RutaDirectorioLog, "Logs_GUI.log"); } }
        [XmlIgnore]
        public string RutaArchivoLogSVC { get { return Path.Combine(this.RutaDirectorioLog, "Logs_SVC.log"); } }
        [XmlIgnore]
        public string AsuntoEmail { get { return this.NombreApp + " :: Notificación"; } }
 
        [XmlIgnore]
        public string GuiLoggerName { get { return "GuiLogger"; } }

        [XmlIgnore]
        public bool SendGuiLog { get; set; }


        #endregion Registro de actividad

        #region Parámetros de ejecución

        #region Parámetros de sincronización
        //public bool TareaFacturacionActivada { get; set; }
        //public int AntiguedadMaximaDiasMovimientosSincro { get; set; }
        public string TipoComprobanteAjusteStock { get; set; }
        #endregion Parámetros de sincronización

        #endregion Parámetros de ejecución

        #region Validacion
        [XmlIgnore]
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(this.TangoDBInstanceNameOrigen))
                {
                    if (String.IsNullOrWhiteSpace(this.TangoDBInstanceNameOrigen))
                        return "Debe ingresar el nombre de instancia origen";
                }
                else if (columnName == nameof(this.TangoDBUserOrigen))
                {
                    if (String.IsNullOrWhiteSpace(this.TangoDBUserOrigen))
                        return "Debe ingresar el nombre de usuario para la conexión a la DB Origen";
                }
                else if (columnName == nameof(this.TangoDBNameOrigen))
                {
                    if (String.IsNullOrWhiteSpace(this.TangoDBNameOrigen))
                        return "Debe ingresar el nombre de la base de datos origen";
                }
                else if (columnName == nameof(this.TimeoutSqlCommandEnSegundosOrigen))
                {
                    if (this.TimeoutSqlCommandEnSegundosOrigen < 0 || this.TimeoutSqlCommandEnSegundosOrigen > 1200)
                        return "El valor  del timeout para la conexión origen debe estar entre 0 y 1200";
                }

                if (columnName == nameof(this.TangoDBInstanceNameDestino))
                {
                    if (String.IsNullOrWhiteSpace(this.TangoDBInstanceNameDestino))
                        return "Debe ingresar el nombre de instancia destino";
                }
                else if (columnName == nameof(this.TangoDBUserDestino))
                {
                    if (String.IsNullOrWhiteSpace(this.TangoDBUserDestino))
                        return "Debe ingresar el nombre de usuario para la conexión a la DB Destino";
                }
                else if (columnName == nameof(this.TangoDBNameDestino))
                {
                    if (String.IsNullOrWhiteSpace(this.TangoDBNameDestino))
                        return "Debe ingresar el nombre de la base de datos destino";
                }
                else if (columnName == nameof(this.TimeoutSqlCommandEnSegundosDestino))
                {
                    if (this.TimeoutSqlCommandEnSegundosDestino < 0 || this.TimeoutSqlCommandEnSegundosDestino > 1200)
                        return "El valor  del timeout para la conexión destino debe estar entre 0 y 1200";
                }


                else if (columnName == nameof(this.TipoComprobanteAjusteStock))
                {
                    if (String.IsNullOrWhiteSpace(this.TipoComprobanteAjusteStock))
                        return "Debe ingresar un valor para el tipo de comprobante de ajuste de stock";
                }
                //else if (columnName == nameof(this.AntiguedadMaximaDiasMovimientosSincro))
                //{
                //    if (this.AntiguedadMaximaDiasMovimientosSincro < -1 || this.AntiguedadMaximaDiasMovimientosSincro > 9999)
                //        return "El valor de antiguedad máxima en días de los movimientos origen debe estar entre -1 y 9999";
                //}

                else if (columnName == nameof(this.ServidorMail))
                {
                    if (this.AlertaViaMailErroresActivada && String.IsNullOrEmpty(this.ServidorMail))
                        return "Debe especificar el nombre del servidor de correo";
                    return null;
                }
                else if (columnName == nameof(this.PuertoMail))
                {
                    if (this.AlertaViaMailErroresActivada && (this.PuertoMail < 1 || this.PuertoMail > 65535))
                        return "El puerto para de envío de correos debe estar entre 1 y 65535";
                    return null;
                }
                else if (columnName == nameof(this.UsuarioMail))
                {
                    if (this.AlertaViaMailErroresActivada && String.IsNullOrEmpty(this.UsuarioMail))
                        return "Debe especificar el nombre de usuario de la cuenta de correo";
                    return null;
                }
                else if (columnName == nameof(this.PasswordMail))
                {
                    if (this.AlertaViaMailErroresActivada && String.IsNullOrEmpty(this.PasswordMail))
                        return "Debe especificar el nombre la contraseña de la cuenta de correo";
                    return null;
                }
                else if (columnName == nameof(this.DireccionMail))
                {
                    if (this.AlertaViaMailErroresActivada && String.IsNullOrEmpty(this.DireccionMail))
                        return "Debe completar la dirección de correo";
                    return null;
                }
                else if (columnName == nameof(this.DestinatariosMailAlertas))
                {
                    if (this.AlertaViaMailErroresActivada && String.IsNullOrEmpty(this.DestinatariosMailAlertas))
                        return "Debe completar el o los destinatarios";
                    return null;
                }
                else if (columnName == nameof(this.AlertasFlushInterval))
                {
                    if (this.AlertasFlushInterval == 0)
                        return "El intervalo de envío de alertas en minutos debe ser mayor a cero";
                    return null;
                }
                return null;
            }
        }

        public string validarNegocio()
        {
            //if(this.CodCuentaTesoreriaCobranza == this.CodCuentaTesoreriaPrincipal)
            //    return "La cuenta de tesorería principal no puede ser igual a la cuenta de tesorería para cobranza";
            //else if (this.CodCuentaContableTesoreriaDebe == this.CodCuentaContableTesoreriaHaber)
            //    return "La cuenta contable para Tesorería (debe) no puede ser igual a la cuenta contable para Tesorería (haber)";
            //else if (this.CodCuentaContableVentasTotal == this.CodCuentaContableVentasGravado)
            //    return "La cuenta contable para el total de ventas no puede ser igual a la cuenta contable para el gravado";
            //else if (this.CodCuentaContableVentasGravado == this.CodCuentaContableVentasIVA)
            //    return "La cuenta contable para el gravado de ventas no puede ser igual a la cuenta contable para el IVA";
            //else if (this.CodTipoAuxiliar1 == this.CodTipoAuxiliar2)
            //    return "El código de tipo de auxiliar 1 no puede ser igual al código de tipo de auxiliar 2";

            return null;
        }


        [XmlIgnore]
        public string Error
        {
            get
            {
                string validation = ValidationHelper.IsValid(this, "Configuración");
                if (String.IsNullOrEmpty(validation))
                {
                    return this.validarNegocio();
                }
                else
                    return validation;
            }
        }
        #endregion Validacion

        public void SetupLogGUI(RichTextBox rtbox)
        {
            this.SendGuiLog = true;
            //LogHelper.SetupLog(rtbox, this.RutaArchivoLog, this.NivelLogDetallado);
            LogUtil.SetupLog(this.RutaArchivoLogGUI, this.NivelLogDetallado, (uint) this.MaxLogFileSizeMB, this.GuiLoggerName, rtbox, null);
        }
        public void SetupLogSVC()
        {
            this.SendGuiLog = false;
            LogUtil.SetupLog(this.RutaArchivoLogSVC, this.NivelLogDetallado, (uint)this.MaxLogFileSizeMB, this.GuiLoggerName, null, null);
            //LogHelper.SetupLog(this.RutaArchivoLog, this.NivelLogDetallado, this.AlertaViaMailErroresPedidosActivado, this.EmailLoggerConfigPedidos, this.FlushIntervalPedidos, this.FlushCountPedidos);

        }
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();
            this.Encriptar = true;

            this.PlanificacionPeriodo = 15;
            this.PlanificacionUnidadTiempo = UnidadTiempo.MINUTO;
            this.PlanificacionEjecutaLunes = true;
            this.PlanificacionEjecutaMartes = true;
            this.PlanificacionEjecutaMiercoles = true;
            this.PlanificacionEjecutaJueves = true;
            this.PlanificacionEjecutaViernes = true;
            this.PlanificacionEjecutaSabado = true;
            this.PlanificacionEjecutaDomingo = true;
            this.PlanificacionHoraEjecucion = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.PlanificacionHoraEjecucionDesde = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.PlanificacionHoraEjecucionHasta = new DateTime(2000, 1, 1, 23, 59, 59, 999);
            this.PlanificacionEjecutaAUnaHoraEspecifica = false;
            this.PlanificacionEjecutaCadaXUnidades = true;
            this.PlanificacionEjecucionActivada = false;
            this.NivelLogDetallado = false;
            this.AlertaViaMailErroresActivada = false;
            this.TimeoutSqlCommandEnSegundosOrigen = 600;
            this.TimeoutSqlCommandEnSegundosDestino= 600;
            this.SendGuiLog = false;
            this.MaxLogFileSizeMB = 100;
            this.AlertasFlushInterval = 4 * 60;
            //this.AntiguedadMaximaDiasMovimientosSincro = 30;
        }

        public override bool cargar()
        {
            bool returnValue = base.cargar();
            AppConfigFile = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            if (!this.FechaUltimaNotificacionEmail.HasValue)
                this.FechaUltimaNotificacionEmail = DateTime.Now;
            return returnValue;
        }

        private static string GetAppConfigValue(string keyName)
        {
            if (!AppConfigFile.AppSettings.Settings.AllKeys.ToList().Exists(k => k == keyName))
                AppConfigFile.AppSettings.Settings.Add(keyName, "");

            return AppConfigFile.AppSettings.Settings[keyName].Value;
        }

        private static void SetAppConfigValue(string keyName, string value, bool save = false)
        {
            if (!AppConfigFile.AppSettings.Settings.AllKeys.ToList().Exists(k => k == keyName))
                AppConfigFile.AppSettings.Settings.Add(keyName, "");

            AppConfigFile.AppSettings.Settings[keyName].Value = value;

            if (save)
                AppConfigFile.Save(ConfigurationSaveMode.Modified);

        }
    }


}
