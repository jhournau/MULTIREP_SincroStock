using GC.Utils.Helpers;
using GC.Utils.Logger;
using SincroStock.Comunes;
using SincroStock.Comunes.Negocio;
using SincroStock.Comunes.Utils;
using SincroStock.Servicio.Negocio;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SincroStock.Comunes.Exceptions;
using System.Diagnostics;
using GC.Tango.WinForms;

namespace SincroStock.Administrador
{
    public partial class FormAdministrador : Form
    {
        #region Constantes, Atributos, y Propiedades

        private static ILog _logger = LogManager.GetLogger(typeof(FormAdministrador));

        private ConfigGeneral config;
        private ControladorInterfaz controladorIfc;
        private ControladorServicio controladorSvc;
        private const string TITULO_MSGBOX = "Administrador";
        private Image imageBotonEjecutarProceso;
        private RtbContextMenu rtbContextMenu;
        private EstadoServicio estadoActualServicio;
        private BindingSource bsConfiguracion = new BindingSource();
        private bool ProcesoDeCargaEnEjecucion
        {
            get
            {
                if (controladorIfc != null)
                {
                    return bgwEjecutarProcesoCarga.IsBusy && controladorIfc.ProcesoEnEjecucion;
                }
                return false;
            }
        }
        #endregion Constantes, Atributos, y Propiedades

        #region Constructor

        public FormAdministrador()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region General

        #region Inicializacion Form

        private void setUpForm()
        {
            this.imageBotonEjecutarProceso = btnEjecutarProceso.Image;
            this.Text = config.NombreApp + " v"+ Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ::: Administrador";

            FormAdministrador.CheckForIllegalCrossThreadCalls = false;

            this.tabControlPrincipal.SelectedTab = this.tabEjecucionManual;

            nupFrecAct.Value = config.PlanificacionPeriodo > nupFrecAct.Minimum ? config.PlanificacionPeriodo : nupFrecAct.Minimum;

            rtbContextMenu = new RtbContextMenu();

            this.timerRefreshServiceStatus.Enabled = true;

            controladorIfc.OnComienzoProceso += this.controladorNegocio_OnComienzoProceso;
            controladorIfc.OnCambioEtapa += this.controladorNegocio_OnCambioEtapa;
            controladorIfc.OnPasoEnEtapa += this.controladorNegocio_OnPasoEnEtapa;
            controladorIfc.OnFinProceso += this.controladorNegocio_OnFinProceso;

            bindearConfiguracion();
            this.errorProvider1.DataSource = this.bsConfiguracion;
            this.ValidateChildren(ValidationConstraints.Enabled);
            this.tabControlPrincipal.Selecting -= tabControl_Selecting;
            this.tabControlPrincipal.SelectedTab = this.tabEjecucionManual;
            this.tabControlPrincipal.Selecting += tabControl_Selecting;

        }

        private void bindearConfiguracion()
        {
            this.bsConfiguracion.DataSource = this.config;

            #region Conexión a Tango
            this.txtPasswordSQLOrigen.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.TangoDBPasswordOrigen), false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtServidorSQLOrigen.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.TangoDBInstanceNameOrigen), false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtUsuarioSQLOrigen.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.TangoDBUserOrigen), false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtNombreBDOrigen.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.TangoDBNameOrigen), false, DataSourceUpdateMode.OnPropertyChanged);
            this.nudTimeoutSqlCommandOrigen.DataBindings.Add("Value", this.bsConfiguracion, nameof(config.TimeoutSqlCommandEnSegundosOrigen), false, DataSourceUpdateMode.OnPropertyChanged);

            this.txtPasswordSQLDestino.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.TangoDBPasswordDestino), false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtServidorSQLDestino.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.TangoDBInstanceNameDestino), false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtUsuarioSQLDestino.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.TangoDBUserDestino), false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtNombreBDDestino.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.TangoDBNameDestino), false, DataSourceUpdateMode.OnPropertyChanged);
            this.nudTimeoutSqlCommandDestino.DataBindings.Add("Value", this.bsConfiguracion, nameof(config.TimeoutSqlCommandEnSegundosDestino), false, DataSourceUpdateMode.OnPropertyChanged);

            #endregion


            #region Parámetros de ejecución

            //this.nudAntiguedadMaximaMovimientos.DataBindings.Add("Value", this.bsConfiguracion, nameof(config.AntiguedadMaximaDiasMovimientosSincro), false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtTipoComprobanteAjuste.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.TipoComprobanteAjusteStock), false, DataSourceUpdateMode.OnPropertyChanged);
            
            #endregion Parámetros de ejecución

            #region Registros de actividad

            this.chkAlertasViaEmailActivadas.DataBindings.Add("Checked", this.bsConfiguracion, nameof(config.AlertaViaMailErroresActivada), false, DataSourceUpdateMode.OnPropertyChanged);
            this.optLogNivelDetallado.DataBindings.Add("Checked", this.bsConfiguracion, nameof(config.NivelLogDetallado), false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtRutaArchivoLog.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.RutaDirectorioLog), false, DataSourceUpdateMode.Never);
            this.txtServidorMail.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.ServidorMail), false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtPuertoMail.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.PuertoMail), false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtDireccionMail.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.DireccionMail), false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtUsuarioMail.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.UsuarioMail), false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtPasswordMail.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.PasswordMail), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkSSL.DataBindings.Add("Checked", this.bsConfiguracion, nameof(config.UsaSSL), false, DataSourceUpdateMode.OnPropertyChanged);
            this.nudAlertasErrorMailFlushInterval.DataBindings.Add("Value", this.bsConfiguracion, nameof(config.AlertasFlushInterval), false, DataSourceUpdateMode.OnPropertyChanged);
            this.nudLogFilMaxSize.DataBindings.Add("Value", this.bsConfiguracion, nameof(config.MaxLogFileSizeMB), false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtDestinatariosAlertas.DataBindings.Add("Text", this.bsConfiguracion, nameof(config.DestinatariosMailAlertas), false, DataSourceUpdateMode.OnPropertyChanged);

            #endregion

            #region Ejecucion Planificada

            this.chkLunes.DataBindings.Add("Checked", this.bsConfiguracion, nameof(config.PlanificacionEjecutaLunes), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkMartes.DataBindings.Add("Checked", this.bsConfiguracion, nameof(config.PlanificacionEjecutaMartes), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkMiercoles.DataBindings.Add("Checked", this.bsConfiguracion, nameof(config.PlanificacionEjecutaMiercoles), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkJueves.DataBindings.Add("Checked", this.bsConfiguracion, nameof(config.PlanificacionEjecutaJueves), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkViernes.DataBindings.Add("Checked", this.bsConfiguracion, nameof(config.PlanificacionEjecutaViernes), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkSabado.DataBindings.Add("Checked", this.bsConfiguracion, nameof(config.PlanificacionEjecutaSabado), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkDomingo.DataBindings.Add("Checked", this.bsConfiguracion, nameof(config.PlanificacionEjecutaDomingo), false, DataSourceUpdateMode.OnPropertyChanged);
            this.dtpHoraDesde.DataBindings.Add("Value", this.bsConfiguracion, nameof(config.PlanificacionHoraEjecucionDesde), false, DataSourceUpdateMode.OnPropertyChanged);
            this.dtpHoraHasta.DataBindings.Add("Value", this.bsConfiguracion, nameof(config.PlanificacionHoraEjecucionHasta), false, DataSourceUpdateMode.OnPropertyChanged);
            this.optFrecEjecucionCadaXUnidades.DataBindings.Add("Checked", this.bsConfiguracion, nameof(config.PlanificacionEjecutaCadaXUnidades), false, DataSourceUpdateMode.OnPropertyChanged);
            this.dtpHora.DataBindings.Add("Value", this.bsConfiguracion, nameof(config.PlanificacionHoraEjecucion), false, DataSourceUpdateMode.OnPropertyChanged);
            this.optFrecEjecucionHoraEspecifica.DataBindings.Add("Checked", this.bsConfiguracion, nameof(config.PlanificacionEjecutaAUnaHoraEspecifica), false, DataSourceUpdateMode.Never);

            this.dtpHora.DataBindings.Add("Enabled", optFrecEjecucionHoraEspecifica, "Checked", false, DataSourceUpdateMode.Never);
            this.dtpHoraDesde.DataBindings.Add("Enabled", optFrecEjecucionCadaXUnidades, "Checked", false, DataSourceUpdateMode.Never);
            this.dtpHoraHasta.DataBindings.Add("Enabled", optFrecEjecucionCadaXUnidades, "Checked", false, DataSourceUpdateMode.Never);
            this.cmbUnidadFrecAct.DataBindings.Add("Enabled", optFrecEjecucionCadaXUnidades, "Checked", false, DataSourceUpdateMode.Never);
            this.nupFrecAct.DataBindings.Add("Enabled", optFrecEjecucionCadaXUnidades, "Checked", false, DataSourceUpdateMode.Never);

            this.cmbUnidadFrecAct.DataSource = EnumHelper.ToExtendedList<int>(typeof(UnidadTiempo));
            this.cmbUnidadFrecAct.DisplayMember = "Value";
            this.cmbUnidadFrecAct.DataBindings.Add("SelectedIndex", this.bsConfiguracion, nameof(config.PlanificacionUnidadTiempoInt), false, DataSourceUpdateMode.OnPropertyChanged);
            this.nupFrecAct.Maximum = getMaxValorFrecuencia((UnidadTiempo)config.PlanificacionUnidadTiempoInt);
            this.nupFrecAct.DataBindings.Add("Value", this.bsConfiguracion, nameof(config.PlanificacionPeriodoDec), false, DataSourceUpdateMode.OnPropertyChanged);

            this.chkEjecucionActivadaDesactivada.DataBindings.Add("Checked", this.bsConfiguracion, nameof(config.PlanificacionEjecucionActivada));
            #endregion

            /*Esto se hace para forzar el binding en todas los tabs, dado que de otra manera,
            este no se realizará hasta tanto se navegue a cada tab.
             Ocurre porque los controles contenidos en un tab, no se crean hasta que 
             el tab en cuestión es seleccionado por primera vez.
             Ergo, se producen efectos no deseados, como no poder accionar algunos controles la primera vez que se los utiliza*/
            foreach (TabPage tp in this.tabControlPrincipal.Controls)
            { 
                tp.Show();
            }

        }

        #endregion Inicializacion Form

        #region Validaciones

        private void cleanProviderError(Control parentControl, ErrorProvider errorProv)
        {
            errorProv.SetError(parentControl, "");
            foreach (Control child in parentControl.Controls)
            {
                errorProv.SetError(child, "");
            }
        }

        public bool comprobarValidacionesCampos(Control control)
        {
            if (errorProvider1.GetError(control) != "")
            {
                return false;
            }
            foreach (Control child in control.Controls)
            {
                if (comprobarValidacionesCampos(child) == false)
                    return false;
            }
            return true;
        }

        private void validacionCamposShowErrorMessage()
        {
            MessageBox.Show("Existen uno o más errores en la configuración que es necesario corregir, verifique las marcas en los campos del formulario", TITULO_MSGBOX
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion Validacion varios

        #endregion General

        #region Event Handlers

        #region General

        private void FormAdministrador_Load(object sender, EventArgs e)
        {
            try
            {
                controladorIfc = ControladorInterfaz.Instance;
                controladorSvc = ControladorServicio.Instance;
                config = ConfigGeneral.Instance;
                config.SetupLogGUI(this.txtResultadoProceso);
                setUpForm();
                LogUtil.Log(_logger, Level.Debug, "Form loaded");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, TITULO_MSGBOX, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Impide que se navegue hacia otra pestaña cuando se esta ejecutando el proceso de carga en forma manual
        //o cuando no se tiene permisos
        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (btnEjecutarProceso.Text == "Cancelar")
                e.Cancel = true;
            else
                return;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (comprobarValidacionesCampos(this) == false)
                {
                    validacionCamposShowErrorMessage();
                    return;
                }

                string configValidacionNegocioMsg = config.validarNegocio();
                if (!String.IsNullOrEmpty(configValidacionNegocioMsg))
                {
                    MessageBox.Show(configValidacionNegocioMsg, TITULO_MSGBOX, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                config.guardar();

                config.SetupLogGUI(this.txtResultadoProceso);

                MessageBox.Show("Datos guardados con éxito.", TITULO_MSGBOX
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TITULO_MSGBOX,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FormAdministrador_FormClosing(null, null);
        }

        private void FormAdministrador_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ProcesoDeCargaEnEjecucion)
            {
                MessageBox.Show("El proceso se encuentra en ejecución, debe esperar a que finalice o cancelarlo", TITULO_MSGBOX, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                return;
            }

            else if (MessageBox.Show("¿Confirma que desea salir del Administrador?", TITULO_MSGBOX, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                if (e != null)
                    e.Cancel = true;
                return;
            }

            this.FormClosing -= FormAdministrador_FormClosing;
            Application.Exit();
        }

        #endregion General

        #region Conexion a Tango

        private void btnProbarConexionTango_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje = "";
                if (txtServidorSQLOrigen.Text == "" || txtUsuarioSQLOrigen.Text == "" || txtNombreBDOrigen.Text == ""
                    ||
                    txtServidorSQLDestino.Text == "" || txtUsuarioSQLDestino.Text == "" || txtNombreBDDestino.Text == "")
                    mensaje = "Debe completar todos los datos de conexión.";
                else
                {
                    string resultadoPruebgaOrigen = GC.Utils.SQL.SqlDAO.ProbarConexionSQL(txtServidorSQLOrigen.Text, txtUsuarioSQLOrigen.Text, txtPasswordSQLOrigen.Text, txtNombreBDOrigen.Text, Convert.ToInt32(nudTimeoutSqlCommandOrigen.Value));
                    string resultadoPruebgaDestino = GC.Utils.SQL.SqlDAO.ProbarConexionSQL(txtServidorSQLDestino.Text, txtUsuarioSQLDestino.Text, txtPasswordSQLDestino.Text, txtNombreBDDestino.Text, Convert.ToInt32(nudTimeoutSqlCommandDestino.Value));
                    if (!String.IsNullOrEmpty(resultadoPruebgaOrigen))
                        mensaje += "Conexión SQL Origen no exitosa:" + Environment.NewLine + resultadoPruebgaOrigen;
                    if (!String.IsNullOrEmpty(resultadoPruebgaDestino))
                        mensaje += (mensaje != "" ? Environment.NewLine : "") + "Conexión SQL Destino no exitosa:" + Environment.NewLine + resultadoPruebgaDestino;

                }

                MessageBox.Show(String.IsNullOrEmpty(mensaje) ? "Conexión exitosa" : mensaje, TITULO_MSGBOX, MessageBoxButtons.OK, String.IsNullOrEmpty(mensaje) ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al probar conexión: " + ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Configuración de ejecución

        //Manejador del evento del timer que refresca el textbox que muestra el estado del servicio
        private void timerRefreshServiceStatus_Tick(object sender, EventArgs e)
        {
            actualizarEstadoServicio();
        }

        private void chkEjecucionActivadaDesactivada_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEjecucionActivadaDesactivada.Checked)
            {

                if (!chkLunes.Checked && !chkMartes.Checked && !chkMiercoles.Checked &&
                   !chkJueves.Checked && !chkViernes.Checked && !chkSabado.Checked &&
                   !chkDomingo.Checked)
                {
                    chkEjecucionActivadaDesactivada.CheckedChanged -= chkEjecucionActivadaDesactivada_CheckedChanged;
                    chkEjecucionActivadaDesactivada.Checked = false;
                    chkEjecucionActivadaDesactivada.CheckedChanged += chkEjecucionActivadaDesactivada_CheckedChanged;
                    MessageBox.Show("Debe seleccionar al menos un día de la semana.", TITULO_MSGBOX,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (controladorSvc.EstadoDelServicio == EstadoServicio.NO_INSTALADO)
                    {
                        if (DialogResult.Yes == MessageBox.Show("El servicio no se encuentra instalado. ¿Desea instalarlo?", TITULO_MSGBOX,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            this.btnInstalarServicio_Click(null, null);
                    }
                }
            }

        }

        private void cmbUnidadFrecAct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int max = getMaxValorFrecuencia((UnidadTiempo)cmbUnidadFrecAct.SelectedIndex);
            if (this.config.PlanificacionPeriodoDec > Convert.ToDecimal(max))
                this.config.PlanificacionPeriodoDec = max;
            nupFrecAct.Maximum = max;
        }

        private void btnInstalarServicio_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                controladorSvc.instalarServicio();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, TITULO_MSGBOX, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesinstalarServicio_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                controladorSvc.desinstalarServicio();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIniciarServicio_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                controladorSvc.iniciarServicio();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, TITULO_MSGBOX, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDetenerServicio_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                controladorSvc.detenerServicio();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, TITULO_MSGBOX, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void actualizarEstadoServicio()
        {
            try
            {
                EstadoServicio estadoServicio = controladorSvc.EstadoDelServicio;

                if (txtEstadoServicio.Text == "" || estadoServicio != estadoActualServicio)
                {
                    txtEstadoServicio.Text = EnumHelper.GetDescription(estadoServicio);

                    bool servicioInstalado = !(estadoServicio == EstadoServicio.NO_INSTALADO);
                    btnDesinstalarServicio.Enabled = (estadoServicio == EstadoServicio.DETENIDO);
                    btnInstalarServicio.Enabled = !servicioInstalado;
                    btnIniciarServicio.Enabled = false;
                    btnDetenerServicio.Enabled = false;

                    if (servicioInstalado)
                    {
                        if (estadoServicio == EstadoServicio.INICIADO ||
                            estadoServicio == EstadoServicio.INICIANDO ||
                            estadoServicio == EstadoServicio.REANUDANDO)
                            btnDetenerServicio.Enabled = true;
                        else if (estadoServicio == EstadoServicio.DETENIDO)
                        {
                            btnIniciarServicio.Enabled = true;
                        }
                    }

                    estadoActualServicio = estadoServicio;
                }
            }
            catch
            {
                if (txtEstadoServicio.Text != "No pudo obtenerse")
                    txtEstadoServicio.Text = "No pudo obtenerse";
            }

        }

        private int getMaxValorFrecuencia(UnidadTiempo unidad)
        {
            switch (unidad)
            {
                case UnidadTiempo.SEGUNDO: return 604800;
                case UnidadTiempo.MINUTO: return 10080;
                default: return 168;
            }
        }

        private void dtpHoraDesde_Validating(object sender, CancelEventArgs e)
        {
            //if (dtpHoraDesde.Value > dtpHoraHasta.Value)
            //{
            //    MessageBox.Show("La hora desde no puede ser mayor a la hora hasta", TITULO_MSGBOX, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    e.Cancel = true;
            //}
        }

        private void dtpHoraHasta_Validating(object sender, CancelEventArgs e)
        {
            //if (dtpHoraHasta.Value < dtpHoraDesde.Value)
            //{
            //    MessageBox.Show("La hora hasta no puede ser menor a la hora desde", TITULO_MSGBOX, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    e.Cancel = true;
            //}
        }

        private void chkDIa_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkLunes.Checked && !chkMartes.Checked && !chkMiercoles.Checked &&
               !chkJueves.Checked && !chkViernes.Checked && !chkSabado.Checked &&
               !chkDomingo.Checked && chkEjecucionActivadaDesactivada.Checked)
            {
                ((CheckBox)sender).CheckedChanged -= chkDIa_CheckedChanged;
                ((CheckBox)sender).Checked = true;
                ((CheckBox)sender).CheckedChanged += chkDIa_CheckedChanged;
                MessageBox.Show("Al menos un día de la semana debe quedar seleccionado ya que la ejecución automática está activa", TITULO_MSGBOX,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion Configuración de ejecución

        #region Parametros de comprobantes

        //private void txtTopeVentas_Validating(object sender, CancelEventArgs e)
        //{
        //    Decimal valor;
        //    if (!Decimal.TryParse(((TextBoxBase)sender).Text, out valor))
        //        e.Cancel = true;
        //}

        #endregion Parametros de comprobantes

        #region Registro de actividad

        private void btnVerLogs_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(config.RutaDirectorioLog))
                    MessageBox.Show("Directorio de logs inexistente", TITULO_MSGBOX, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    ProcessStartInfo processInfo = new ProcessStartInfo();
                    Process.Start(config.RutaDirectorioLog);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el directorio de logs:\n\n" + ex.Message, TITULO_MSGBOX, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnProbarEnvioMail_Click(object sender, EventArgs e)
        {
            UtilsIFC.EnviarMailNotificacion(config.AsuntoEmail + " (prueba de envío)", "", this.txtDestinatariosAlertas.Text);
        }


        #endregion Registro de actividad

        #region Ejecución Manual

        private void btnEjecutarProceso_Click(object sender, EventArgs e)
        {
            if (this.btnEjecutarProceso.Text == "Cancelar")
            {
                this.btnEjecutarProceso.Enabled = false;
                this.btnEjecutarProceso.Text = "Cancelando...";
                controladorIfc.InterrumpirProceso = true;
            }
            else //"Aceptar"
            {
                if (comprobarValidacionesCampos(this) == false)
                {
                    validacionCamposShowErrorMessage();
                    return;
                }
                config.guardar();

                this.txtResultadoProceso.Clear();
                this.btnEjecutarProceso.Text = "Cancelar";
                this.btnEjecutarProceso.Image = null;
                this.btnSalir.Enabled = false;
                this.btnGuardar.Enabled = false;
                this.bgwEjecutarProcesoCarga.RunWorkerAsync();
            }
        }

        private void bgwEjecutarProcesoCarga_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                controladorIfc.Modo = ModoEjecucion.GUI;
                controladorIfc.iniciarTarea();
                e.Result = null;
            }
            catch (UserAbortException uae)
            {
                e.Result = uae;
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void bgwEjecutarProcesoCarga_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                btnGuardar.Enabled = true;
                btnSalir.Enabled = true;
                btnEjecutarProceso.Enabled = true;
                this.btnEjecutarProceso.Text = "Iniciar";
                this.btnEjecutarProceso.Image = this.imageBotonEjecutarProceso;
                if (e.Result != null)
                {
                    if (e.Result.GetType() == typeof(UserAbortException))
                        LogUtil.LogWithGui(_logger, Level.Info, "Proceso cancelado", true);
                    else
                        LogUtil.LogWithGui(_logger, Level.Error, e.Result.ToString(), true);
                }
                else
                    LogUtil.LogWithGui(_logger, Level.Info, "Proceso finalizado", true);
            }
            catch (Exception ex)
            {
                LogUtil.LogWithGui(_logger, Level.Error, ex.Message, true);
            }
        }

        private void txtResultadoProceso_MouseDown(object sender, MouseEventArgs e)
        {
            rtbContextMenu.Display(txtResultadoProceso, e);
        }

        #endregion Ejecución Manual

        #region ControladorNegocio Progreso

        private void controladorNegocio_OnComienzoProceso(int cantidadDeEtapas)
        {
            pgbEjecucionManualProgresoTotal.Maximum = cantidadDeEtapas;
            pgbEjecucionManualProgresoTotal.Value = 0;
        }

        private void controladorNegocio_OnFinProceso()
        {
            pgbEjecucionManualProgresoTotal.Value = 0;
            pgbEjecucionManualProgresoEtapa.Value = 0;
            lblEjecucionManualDescripcionEtapa.Text = "";
            lblEjecucionManualDescripcionPasoEtapa.Text = "";
        }

        private void controladorNegocio_OnCambioEtapa(string descripcionEtapa, int cantidadDePasos)
        {
            pgbEjecucionManualProgresoEtapa.Maximum = cantidadDePasos;
            pgbEjecucionManualProgresoEtapa.Value = 0;
            if (pgbEjecucionManualProgresoTotal.Maximum > 0)
                pgbEjecucionManualProgresoTotal.Value += 1;
            lblEjecucionManualDescripcionEtapa.Text = Math.Round((((decimal)pgbEjecucionManualProgresoTotal.Value / (decimal)pgbEjecucionManualProgresoTotal.Maximum) * 100), 0).ToString() + "% | " + descripcionEtapa;
        }

        private void controladorNegocio_OnPasoEnEtapa(string descripcionPaso)
        {
            if (pgbEjecucionManualProgresoEtapa.Maximum > 0)
                pgbEjecucionManualProgresoEtapa.Value += 1;
            lblEjecucionManualDescripcionPasoEtapa.Text = Math.Round((((decimal)pgbEjecucionManualProgresoEtapa.Value / (decimal)pgbEjecucionManualProgresoEtapa.Maximum) * 100), 0).ToString() + "% | " + descripcionPaso;
        }




        #endregion ControladorNegocio Progreso



        #endregion Event Handlers


        private void optFrecEjecucionHoraEspecifica_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
