namespace SincroStock.Administrador
{
    partial class FormAdministrador
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdministrador));
            this.txtServidorSQLOrigen = new System.Windows.Forms.TextBox();
            this.txtUsuarioSQLOrigen = new System.Windows.Forms.TextBox();
            this.txtPasswordSQLOrigen = new System.Windows.Forms.TextBox();
            this.lblServerSQL = new System.Windows.Forms.Label();
            this.lblNombreUsr = new System.Windows.Forms.Label();
            this.lblContrasenia = new System.Windows.Forms.Label();
            this.gbxConexionTangoBDOrigen = new System.Windows.Forms.GroupBox();
            this.label55 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.txtNombreBDOrigen = new System.Windows.Forms.TextBox();
            this.nudTimeoutSqlCommandOrigen = new System.Windows.Forms.NumericUpDown();
            this.tabControlPrincipal = new System.Windows.Forms.TabControl();
            this.tabConexionTango = new System.Windows.Forms.TabPage();
            this.gbxConexionTangoBDDestino = new System.Windows.Forms.GroupBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.txtNombreBDDestino = new System.Windows.Forms.TextBox();
            this.nudTimeoutSqlCommandDestino = new System.Windows.Forms.NumericUpDown();
            this.label45 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.txtPasswordSQLDestino = new System.Windows.Forms.TextBox();
            this.txtUsuarioSQLDestino = new System.Windows.Forms.TextBox();
            this.txtServidorSQLDestino = new System.Windows.Forms.TextBox();
            this.btnProbarConexionTango = new System.Windows.Forms.Button();
            this.tabParametrosSincro = new System.Windows.Forms.TabPage();
            this.tblParametrosSincronizacion = new System.Windows.Forms.TableLayoutPanel();
            this.label15 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.nudAntiguedadMaximaMovimientos = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTipoComprobanteAjuste = new System.Windows.Forms.TextBox();
            this.tabRegistroDeActividad = new System.Windows.Forms.TabPage();
            this.gbxAlertasViaEmail = new System.Windows.Forms.GroupBox();
            this.pnlDatosAlertaEmail = new System.Windows.Forms.Panel();
            this.nudAlertasErrorMailFlushInterval = new System.Windows.Forms.NumericUpDown();
            this.label54 = new System.Windows.Forms.Label();
            this.txtDestinatariosAlertas = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.chkSSL = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtServidorMail = new System.Windows.Forms.TextBox();
            this.btnProbarEnvioMail = new System.Windows.Forms.Button();
            this.txtPuertoMail = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtDireccionMail = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtUsuarioMail = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtPasswordMail = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.chkAlertasViaEmailActivadas = new System.Windows.Forms.CheckBox();
            this.gbxLogNivel = new System.Windows.Forms.GroupBox();
            this.label37 = new System.Windows.Forms.Label();
            this.btnVerLogs = new System.Windows.Forms.Button();
            this.label36 = new System.Windows.Forms.Label();
            this.nudLogFilMaxSize = new System.Windows.Forms.NumericUpDown();
            this.optLogNivelSoloErrores = new System.Windows.Forms.RadioButton();
            this.txtRutaArchivoLog = new System.Windows.Forms.TextBox();
            this.optLogNivelDetallado = new System.Windows.Forms.RadioButton();
            this.tabEjecucionPlanificada = new System.Windows.Forms.TabPage();
            this.gbxOpcionesDelServicio = new System.Windows.Forms.GroupBox();
            this.txtEstadoServicio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnIniciarServicio = new System.Windows.Forms.Button();
            this.btnDetenerServicio = new System.Windows.Forms.Button();
            this.btnDesinstalarServicio = new System.Windows.Forms.Button();
            this.btnInstalarServicio = new System.Windows.Forms.Button();
            this.gbxFrecuenciaEjecucion = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.optFrecEjecucionHoraEspecifica = new System.Windows.Forms.RadioButton();
            this.optFrecEjecucionCadaXUnidades = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkMartes = new System.Windows.Forms.CheckBox();
            this.chkMiercoles = new System.Windows.Forms.CheckBox();
            this.chkDomingo = new System.Windows.Forms.CheckBox();
            this.chkJueves = new System.Windows.Forms.CheckBox();
            this.chkSabado = new System.Windows.Forms.CheckBox();
            this.chkViernes = new System.Windows.Forms.CheckBox();
            this.chkLunes = new System.Windows.Forms.CheckBox();
            this.dtpHoraHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpHoraDesde = new System.Windows.Forms.DateTimePicker();
            this.dtpHora = new System.Windows.Forms.DateTimePicker();
            this.label42 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.nupFrecAct = new System.Windows.Forms.NumericUpDown();
            this.cmbUnidadFrecAct = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlEjecucionActivadaDesactivada = new System.Windows.Forms.Panel();
            this.chkEjecucionActivadaDesactivada = new System.Windows.Forms.CheckBox();
            this.tabEjecucionManual = new System.Windows.Forms.TabPage();
            this.pgbEjecucionManualProgresoEtapa = new System.Windows.Forms.ProgressBar();
            this.pgbEjecucionManualProgresoTotal = new System.Windows.Forms.ProgressBar();
            this.lblEjecucionManualDescripcionPasoEtapa = new System.Windows.Forms.Label();
            this.lblEjecucionManualDescripcionEtapa = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtResultadoProceso = new System.Windows.Forms.RichTextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.btnEjecutarProceso = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.bgwEjecutarProcesoCarga = new System.ComponentModel.BackgroundWorker();
            this.timerRefreshServiceStatus = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsSLToolTipText = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gbxConexionTangoBDOrigen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeoutSqlCommandOrigen)).BeginInit();
            this.tabControlPrincipal.SuspendLayout();
            this.tabConexionTango.SuspendLayout();
            this.gbxConexionTangoBDDestino.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeoutSqlCommandDestino)).BeginInit();
            this.tabParametrosSincro.SuspendLayout();
            this.tblParametrosSincronizacion.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAntiguedadMaximaMovimientos)).BeginInit();
            this.tabRegistroDeActividad.SuspendLayout();
            this.gbxAlertasViaEmail.SuspendLayout();
            this.pnlDatosAlertaEmail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlertasErrorMailFlushInterval)).BeginInit();
            this.gbxLogNivel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogFilMaxSize)).BeginInit();
            this.tabEjecucionPlanificada.SuspendLayout();
            this.gbxOpcionesDelServicio.SuspendLayout();
            this.gbxFrecuenciaEjecucion.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupFrecAct)).BeginInit();
            this.pnlEjecucionActivadaDesactivada.SuspendLayout();
            this.tabEjecucionManual.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtServidorSQLOrigen
            // 
            this.txtServidorSQLOrigen.Location = new System.Drawing.Point(131, 21);
            this.txtServidorSQLOrigen.Name = "txtServidorSQLOrigen";
            this.txtServidorSQLOrigen.Size = new System.Drawing.Size(211, 20);
            this.txtServidorSQLOrigen.TabIndex = 6;
            this.txtServidorSQLOrigen.Tag = "";
            // 
            // txtUsuarioSQLOrigen
            // 
            this.txtUsuarioSQLOrigen.Location = new System.Drawing.Point(131, 47);
            this.txtUsuarioSQLOrigen.Name = "txtUsuarioSQLOrigen";
            this.txtUsuarioSQLOrigen.Size = new System.Drawing.Size(211, 20);
            this.txtUsuarioSQLOrigen.TabIndex = 11;
            this.txtUsuarioSQLOrigen.Tag = "";
            // 
            // txtPasswordSQLOrigen
            // 
            this.txtPasswordSQLOrigen.Location = new System.Drawing.Point(131, 73);
            this.txtPasswordSQLOrigen.Name = "txtPasswordSQLOrigen";
            this.txtPasswordSQLOrigen.Size = new System.Drawing.Size(211, 20);
            this.txtPasswordSQLOrigen.TabIndex = 16;
            this.txtPasswordSQLOrigen.Tag = "";
            this.txtPasswordSQLOrigen.UseSystemPasswordChar = true;
            // 
            // lblServerSQL
            // 
            this.lblServerSQL.AutoSize = true;
            this.lblServerSQL.Location = new System.Drawing.Point(15, 24);
            this.lblServerSQL.Name = "lblServerSQL";
            this.lblServerSQL.Size = new System.Drawing.Size(73, 13);
            this.lblServerSQL.TabIndex = 0;
            this.lblServerSQL.Text = "Servidor SQL:";
            // 
            // lblNombreUsr
            // 
            this.lblNombreUsr.AutoSize = true;
            this.lblNombreUsr.Location = new System.Drawing.Point(14, 50);
            this.lblNombreUsr.Name = "lblNombreUsr";
            this.lblNombreUsr.Size = new System.Drawing.Size(101, 13);
            this.lblNombreUsr.TabIndex = 0;
            this.lblNombreUsr.Text = "Nombre de Usuario:";
            // 
            // lblContrasenia
            // 
            this.lblContrasenia.AutoSize = true;
            this.lblContrasenia.Location = new System.Drawing.Point(14, 76);
            this.lblContrasenia.Name = "lblContrasenia";
            this.lblContrasenia.Size = new System.Drawing.Size(64, 13);
            this.lblContrasenia.TabIndex = 0;
            this.lblContrasenia.Text = "Contraseña:";
            // 
            // gbxConexionTangoBDOrigen
            // 
            this.gbxConexionTangoBDOrigen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxConexionTangoBDOrigen.Controls.Add(this.label55);
            this.gbxConexionTangoBDOrigen.Controls.Add(this.label10);
            this.gbxConexionTangoBDOrigen.Controls.Add(this.label53);
            this.gbxConexionTangoBDOrigen.Controls.Add(this.txtNombreBDOrigen);
            this.gbxConexionTangoBDOrigen.Controls.Add(this.nudTimeoutSqlCommandOrigen);
            this.gbxConexionTangoBDOrigen.Controls.Add(this.lblContrasenia);
            this.gbxConexionTangoBDOrigen.Controls.Add(this.lblNombreUsr);
            this.gbxConexionTangoBDOrigen.Controls.Add(this.lblServerSQL);
            this.gbxConexionTangoBDOrigen.Controls.Add(this.txtPasswordSQLOrigen);
            this.gbxConexionTangoBDOrigen.Controls.Add(this.txtUsuarioSQLOrigen);
            this.gbxConexionTangoBDOrigen.Controls.Add(this.txtServidorSQLOrigen);
            this.gbxConexionTangoBDOrigen.Location = new System.Drawing.Point(10, 7);
            this.gbxConexionTangoBDOrigen.Name = "gbxConexionTangoBDOrigen";
            this.gbxConexionTangoBDOrigen.Size = new System.Drawing.Size(378, 173);
            this.gbxConexionTangoBDOrigen.TabIndex = 11;
            this.gbxConexionTangoBDOrigen.TabStop = false;
            this.gbxConexionTangoBDOrigen.Text = "Conexión a la Base de Datos Origen";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(196, 134);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(116, 13);
            this.label55.TabIndex = 24;
            this.label55.Text = "segundo(s) (0 = infinito)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 103);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Nombre BD Origen:";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(14, 134);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(96, 13);
            this.label53.TabIndex = 23;
            this.label53.Text = "SQL Cmd Timeout:";
            // 
            // txtNombreBDOrigen
            // 
            this.txtNombreBDOrigen.Location = new System.Drawing.Point(131, 100);
            this.txtNombreBDOrigen.Name = "txtNombreBDOrigen";
            this.txtNombreBDOrigen.Size = new System.Drawing.Size(211, 20);
            this.txtNombreBDOrigen.TabIndex = 21;
            this.txtNombreBDOrigen.Tag = "";
            // 
            // nudTimeoutSqlCommandOrigen
            // 
            this.nudTimeoutSqlCommandOrigen.Location = new System.Drawing.Point(131, 132);
            this.nudTimeoutSqlCommandOrigen.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.nudTimeoutSqlCommandOrigen.Name = "nudTimeoutSqlCommandOrigen";
            this.nudTimeoutSqlCommandOrigen.Size = new System.Drawing.Size(59, 20);
            this.nudTimeoutSqlCommandOrigen.TabIndex = 25;
            this.nudTimeoutSqlCommandOrigen.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tabControlPrincipal
            // 
            this.tabControlPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlPrincipal.Controls.Add(this.tabConexionTango);
            this.tabControlPrincipal.Controls.Add(this.tabParametrosSincro);
            this.tabControlPrincipal.Controls.Add(this.tabRegistroDeActividad);
            this.tabControlPrincipal.Controls.Add(this.tabEjecucionPlanificada);
            this.tabControlPrincipal.Controls.Add(this.tabEjecucionManual);
            this.tabControlPrincipal.Location = new System.Drawing.Point(30, 12);
            this.tabControlPrincipal.Name = "tabControlPrincipal";
            this.tabControlPrincipal.SelectedIndex = 0;
            this.tabControlPrincipal.Size = new System.Drawing.Size(877, 496);
            this.tabControlPrincipal.TabIndex = 11;
            this.tabControlPrincipal.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl_Selecting);
            // 
            // tabConexionTango
            // 
            this.tabConexionTango.Controls.Add(this.gbxConexionTangoBDDestino);
            this.tabConexionTango.Controls.Add(this.gbxConexionTangoBDOrigen);
            this.tabConexionTango.Controls.Add(this.btnProbarConexionTango);
            this.tabConexionTango.Location = new System.Drawing.Point(4, 22);
            this.tabConexionTango.Name = "tabConexionTango";
            this.tabConexionTango.Padding = new System.Windows.Forms.Padding(3);
            this.tabConexionTango.Size = new System.Drawing.Size(869, 470);
            this.tabConexionTango.TabIndex = 0;
            this.tabConexionTango.Text = "Conexión a Tango";
            this.tabConexionTango.UseVisualStyleBackColor = true;
            // 
            // gbxConexionTangoBDDestino
            // 
            this.gbxConexionTangoBDDestino.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxConexionTangoBDDestino.Controls.Add(this.label35);
            this.gbxConexionTangoBDDestino.Controls.Add(this.label41);
            this.gbxConexionTangoBDDestino.Controls.Add(this.label44);
            this.gbxConexionTangoBDDestino.Controls.Add(this.txtNombreBDDestino);
            this.gbxConexionTangoBDDestino.Controls.Add(this.nudTimeoutSqlCommandDestino);
            this.gbxConexionTangoBDDestino.Controls.Add(this.label45);
            this.gbxConexionTangoBDDestino.Controls.Add(this.label47);
            this.gbxConexionTangoBDDestino.Controls.Add(this.label49);
            this.gbxConexionTangoBDDestino.Controls.Add(this.txtPasswordSQLDestino);
            this.gbxConexionTangoBDDestino.Controls.Add(this.txtUsuarioSQLDestino);
            this.gbxConexionTangoBDDestino.Controls.Add(this.txtServidorSQLDestino);
            this.gbxConexionTangoBDDestino.Location = new System.Drawing.Point(411, 6);
            this.gbxConexionTangoBDDestino.Name = "gbxConexionTangoBDDestino";
            this.gbxConexionTangoBDDestino.Size = new System.Drawing.Size(378, 173);
            this.gbxConexionTangoBDDestino.TabIndex = 20;
            this.gbxConexionTangoBDDestino.TabStop = false;
            this.gbxConexionTangoBDDestino.Text = "Conexión a la Base de Datos Destino";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(196, 134);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(116, 13);
            this.label35.TabIndex = 24;
            this.label35.Text = "segundo(s) (0 = infinito)";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(14, 103);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(104, 13);
            this.label41.TabIndex = 0;
            this.label41.Text = "Nombre BD Destino:";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(14, 134);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(96, 13);
            this.label44.TabIndex = 23;
            this.label44.Text = "SQL Cmd Timeout:";
            // 
            // txtNombreBDDestino
            // 
            this.txtNombreBDDestino.Location = new System.Drawing.Point(131, 100);
            this.txtNombreBDDestino.Name = "txtNombreBDDestino";
            this.txtNombreBDDestino.Size = new System.Drawing.Size(211, 20);
            this.txtNombreBDDestino.TabIndex = 21;
            this.txtNombreBDDestino.Tag = "";
            // 
            // nudTimeoutSqlCommandDestino
            // 
            this.nudTimeoutSqlCommandDestino.Location = new System.Drawing.Point(131, 132);
            this.nudTimeoutSqlCommandDestino.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.nudTimeoutSqlCommandDestino.Name = "nudTimeoutSqlCommandDestino";
            this.nudTimeoutSqlCommandDestino.Size = new System.Drawing.Size(59, 20);
            this.nudTimeoutSqlCommandDestino.TabIndex = 25;
            this.nudTimeoutSqlCommandDestino.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(14, 76);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(64, 13);
            this.label45.TabIndex = 0;
            this.label45.Text = "Contraseña:";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(14, 50);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(101, 13);
            this.label47.TabIndex = 0;
            this.label47.Text = "Nombre de Usuario:";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(15, 24);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(73, 13);
            this.label49.TabIndex = 0;
            this.label49.Text = "Servidor SQL:";
            // 
            // txtPasswordSQLDestino
            // 
            this.txtPasswordSQLDestino.Location = new System.Drawing.Point(131, 73);
            this.txtPasswordSQLDestino.Name = "txtPasswordSQLDestino";
            this.txtPasswordSQLDestino.Size = new System.Drawing.Size(211, 20);
            this.txtPasswordSQLDestino.TabIndex = 16;
            this.txtPasswordSQLDestino.Tag = "";
            this.txtPasswordSQLDestino.UseSystemPasswordChar = true;
            // 
            // txtUsuarioSQLDestino
            // 
            this.txtUsuarioSQLDestino.Location = new System.Drawing.Point(131, 47);
            this.txtUsuarioSQLDestino.Name = "txtUsuarioSQLDestino";
            this.txtUsuarioSQLDestino.Size = new System.Drawing.Size(211, 20);
            this.txtUsuarioSQLDestino.TabIndex = 11;
            this.txtUsuarioSQLDestino.Tag = "";
            // 
            // txtServidorSQLDestino
            // 
            this.txtServidorSQLDestino.Location = new System.Drawing.Point(131, 21);
            this.txtServidorSQLDestino.Name = "txtServidorSQLDestino";
            this.txtServidorSQLDestino.Size = new System.Drawing.Size(211, 20);
            this.txtServidorSQLDestino.TabIndex = 6;
            this.txtServidorSQLDestino.Tag = "";
            // 
            // btnProbarConexionTango
            // 
            this.btnProbarConexionTango.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProbarConexionTango.Image = global::SincroStock.Administrador.Properties.Resources.tango;
            this.btnProbarConexionTango.Location = new System.Drawing.Point(10, 196);
            this.btnProbarConexionTango.Name = "btnProbarConexionTango";
            this.btnProbarConexionTango.Size = new System.Drawing.Size(121, 31);
            this.btnProbarConexionTango.TabIndex = 30;
            this.btnProbarConexionTango.Text = "Probar &conexión";
            this.btnProbarConexionTango.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProbarConexionTango.UseVisualStyleBackColor = true;
            this.btnProbarConexionTango.Click += new System.EventHandler(this.btnProbarConexionTango_Click);
            // 
            // tabParametrosSincro
            // 
            this.tabParametrosSincro.Controls.Add(this.tblParametrosSincronizacion);
            this.tabParametrosSincro.Location = new System.Drawing.Point(4, 22);
            this.tabParametrosSincro.Name = "tabParametrosSincro";
            this.tabParametrosSincro.Size = new System.Drawing.Size(869, 470);
            this.tabParametrosSincro.TabIndex = 8;
            this.tabParametrosSincro.Text = "Parámetros de Sincronización";
            this.tabParametrosSincro.UseVisualStyleBackColor = true;
            // 
            // tblParametrosSincronizacion
            // 
            this.tblParametrosSincronizacion.ColumnCount = 7;
            this.tblParametrosSincronizacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 196F));
            this.tblParametrosSincronizacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 206F));
            this.tblParametrosSincronizacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblParametrosSincronizacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tblParametrosSincronizacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tblParametrosSincronizacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tblParametrosSincronizacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblParametrosSincronizacion.Controls.Add(this.label4, 0, 0);
            this.tblParametrosSincronizacion.Controls.Add(this.txtTipoComprobanteAjuste, 1, 0);
            this.tblParametrosSincronizacion.Controls.Add(this.label15, 0, 1);
            this.tblParametrosSincronizacion.Controls.Add(this.panel5, 1, 1);
            this.tblParametrosSincronizacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblParametrosSincronizacion.Location = new System.Drawing.Point(0, 0);
            this.tblParametrosSincronizacion.Margin = new System.Windows.Forms.Padding(0);
            this.tblParametrosSincronizacion.Name = "tblParametrosSincronizacion";
            this.tblParametrosSincronizacion.RowCount = 16;
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblParametrosSincronizacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblParametrosSincronizacion.Size = new System.Drawing.Size(869, 470);
            this.tblParametrosSincronizacion.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(3, 30);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(190, 30);
            this.label15.TabIndex = 1;
            this.label15.Text = "Antiguedad máxima mov. a sincronizar:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label15.Visible = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.nudAntiguedadMaximaMovimientos);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Location = new System.Drawing.Point(199, 33);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 24);
            this.panel5.TabIndex = 501;
            this.panel5.Visible = false;
            // 
            // nudAntiguedadMaximaMovimientos
            // 
            this.nudAntiguedadMaximaMovimientos.Location = new System.Drawing.Point(4, 1);
            this.nudAntiguedadMaximaMovimientos.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudAntiguedadMaximaMovimientos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudAntiguedadMaximaMovimientos.Name = "nudAntiguedadMaximaMovimientos";
            this.nudAntiguedadMaximaMovimientos.Size = new System.Drawing.Size(68, 20);
            this.nudAntiguedadMaximaMovimientos.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(78, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "días ( -1 = sin máximo)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 30);
            this.label4.TabIndex = 1;
            this.label4.Text = "Tipo comprobante ajuste:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTipoComprobanteAjuste
            // 
            this.txtTipoComprobanteAjuste.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTipoComprobanteAjuste.Location = new System.Drawing.Point(199, 5);
            this.txtTipoComprobanteAjuste.MaxLength = 3;
            this.txtTipoComprobanteAjuste.Name = "txtTipoComprobanteAjuste";
            this.txtTipoComprobanteAjuste.Size = new System.Drawing.Size(72, 20);
            this.txtTipoComprobanteAjuste.TabIndex = 10;
            // 
            // tabRegistroDeActividad
            // 
            this.tabRegistroDeActividad.Controls.Add(this.gbxAlertasViaEmail);
            this.tabRegistroDeActividad.Controls.Add(this.gbxLogNivel);
            this.tabRegistroDeActividad.Location = new System.Drawing.Point(4, 22);
            this.tabRegistroDeActividad.Name = "tabRegistroDeActividad";
            this.tabRegistroDeActividad.Size = new System.Drawing.Size(869, 470);
            this.tabRegistroDeActividad.TabIndex = 6;
            this.tabRegistroDeActividad.Text = "Registro de actividad";
            this.tabRegistroDeActividad.UseVisualStyleBackColor = true;
            // 
            // gbxAlertasViaEmail
            // 
            this.gbxAlertasViaEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxAlertasViaEmail.Controls.Add(this.pnlDatosAlertaEmail);
            this.gbxAlertasViaEmail.Controls.Add(this.chkAlertasViaEmailActivadas);
            this.gbxAlertasViaEmail.Location = new System.Drawing.Point(5, 99);
            this.gbxAlertasViaEmail.Name = "gbxAlertasViaEmail";
            this.gbxAlertasViaEmail.Size = new System.Drawing.Size(858, 292);
            this.gbxAlertasViaEmail.TabIndex = 21;
            this.gbxAlertasViaEmail.TabStop = false;
            this.gbxAlertasViaEmail.Text = "Notificaciones vía E-mail";
            // 
            // pnlDatosAlertaEmail
            // 
            this.pnlDatosAlertaEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDatosAlertaEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDatosAlertaEmail.Controls.Add(this.nudAlertasErrorMailFlushInterval);
            this.pnlDatosAlertaEmail.Controls.Add(this.label54);
            this.pnlDatosAlertaEmail.Controls.Add(this.txtDestinatariosAlertas);
            this.pnlDatosAlertaEmail.Controls.Add(this.label52);
            this.pnlDatosAlertaEmail.Controls.Add(this.label26);
            this.pnlDatosAlertaEmail.Controls.Add(this.chkSSL);
            this.pnlDatosAlertaEmail.Controls.Add(this.label20);
            this.pnlDatosAlertaEmail.Controls.Add(this.txtServidorMail);
            this.pnlDatosAlertaEmail.Controls.Add(this.btnProbarEnvioMail);
            this.pnlDatosAlertaEmail.Controls.Add(this.txtPuertoMail);
            this.pnlDatosAlertaEmail.Controls.Add(this.label22);
            this.pnlDatosAlertaEmail.Controls.Add(this.txtDireccionMail);
            this.pnlDatosAlertaEmail.Controls.Add(this.label23);
            this.pnlDatosAlertaEmail.Controls.Add(this.txtUsuarioMail);
            this.pnlDatosAlertaEmail.Controls.Add(this.label24);
            this.pnlDatosAlertaEmail.Controls.Add(this.txtPasswordMail);
            this.pnlDatosAlertaEmail.Controls.Add(this.label25);
            this.pnlDatosAlertaEmail.Location = new System.Drawing.Point(6, 45);
            this.pnlDatosAlertaEmail.Name = "pnlDatosAlertaEmail";
            this.pnlDatosAlertaEmail.Size = new System.Drawing.Size(846, 233);
            this.pnlDatosAlertaEmail.TabIndex = 20;
            // 
            // nudAlertasErrorMailFlushInterval
            // 
            this.nudAlertasErrorMailFlushInterval.Location = new System.Drawing.Point(170, 119);
            this.nudAlertasErrorMailFlushInterval.Maximum = new decimal(new int[] {
            10080,
            0,
            0,
            0});
            this.nudAlertasErrorMailFlushInterval.Name = "nudAlertasErrorMailFlushInterval";
            this.nudAlertasErrorMailFlushInterval.Size = new System.Drawing.Size(59, 20);
            this.nudAlertasErrorMailFlushInterval.TabIndex = 10;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(235, 121);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(49, 13);
            this.label54.TabIndex = 108;
            this.label54.Text = "minuto(s)";
            // 
            // txtDestinatariosAlertas
            // 
            this.txtDestinatariosAlertas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestinatariosAlertas.Location = new System.Drawing.Point(118, 86);
            this.txtDestinatariosAlertas.Name = "txtDestinatariosAlertas";
            this.txtDestinatariosAlertas.Size = new System.Drawing.Size(535, 20);
            this.txtDestinatariosAlertas.TabIndex = 70;
            this.txtDestinatariosAlertas.Tag = "Destinatario de las alertas (ej. direccion@dominio.com)";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(6, 121);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(158, 13);
            this.label52.TabIndex = 107;
            this.label52.Tag = resources.GetString("label52.Tag");
            this.label52.Text = "Frecuencia máxima notificación:";
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(4, 89);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(71, 13);
            this.label26.TabIndex = 101;
            this.label26.Text = "Destinatarios:";
            // 
            // chkSSL
            // 
            this.chkSSL.AutoSize = true;
            this.chkSSL.Location = new System.Drawing.Point(446, 9);
            this.chkSSL.Name = "chkSSL";
            this.chkSSL.Size = new System.Drawing.Size(77, 17);
            this.chkSSL.TabIndex = 30;
            this.chkSSL.Tag = "Indica si el servidor utiliza el protocolo SSL";
            this.chkSSL.Text = "Utiliza SSL";
            this.chkSSL.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(4, 11);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(97, 13);
            this.label20.TabIndex = 9;
            this.label20.Text = "Servidor de correo:";
            // 
            // txtServidorMail
            // 
            this.txtServidorMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServidorMail.Location = new System.Drawing.Point(118, 8);
            this.txtServidorMail.Name = "txtServidorMail";
            this.txtServidorMail.Size = new System.Drawing.Size(175, 20);
            this.txtServidorMail.TabIndex = 10;
            this.txtServidorMail.Tag = "Dirección del servidor de correo (ej. smtp.miservidor.com.ar)";
            // 
            // btnProbarEnvioMail
            // 
            this.btnProbarEnvioMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProbarEnvioMail.Image = global::SincroStock.Administrador.Properties.Resources.mail_send;
            this.btnProbarEnvioMail.Location = new System.Drawing.Point(7, 172);
            this.btnProbarEnvioMail.Name = "btnProbarEnvioMail";
            this.btnProbarEnvioMail.Size = new System.Drawing.Size(107, 28);
            this.btnProbarEnvioMail.TabIndex = 100;
            this.btnProbarEnvioMail.Text = "&Probar envío";
            this.btnProbarEnvioMail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProbarEnvioMail.UseVisualStyleBackColor = true;
            this.btnProbarEnvioMail.Click += new System.EventHandler(this.btnProbarEnvioMail_Click);
            // 
            // txtPuertoMail
            // 
            this.txtPuertoMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPuertoMail.Location = new System.Drawing.Point(392, 8);
            this.txtPuertoMail.Name = "txtPuertoMail";
            this.txtPuertoMail.Size = new System.Drawing.Size(33, 20);
            this.txtPuertoMail.TabIndex = 20;
            this.txtPuertoMail.Tag = "Puerto del servidor de correo (ej. 25)";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(322, 11);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 13);
            this.label22.TabIndex = 11;
            this.label22.Text = "Puerto:";
            // 
            // txtDireccionMail
            // 
            this.txtDireccionMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDireccionMail.Location = new System.Drawing.Point(118, 60);
            this.txtDireccionMail.Name = "txtDireccionMail";
            this.txtDireccionMail.Size = new System.Drawing.Size(175, 20);
            this.txtDireccionMail.TabIndex = 60;
            this.txtDireccionMail.Tag = "Dirección de la cuenta con que se enviarán las alertas (ej. minombre, ej. minombr" +
    "e@dominio.com)";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(4, 62);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(55, 13);
            this.label23.TabIndex = 13;
            this.label23.Text = "Dirección:";
            // 
            // txtUsuarioMail
            // 
            this.txtUsuarioMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuarioMail.Location = new System.Drawing.Point(118, 34);
            this.txtUsuarioMail.Name = "txtUsuarioMail";
            this.txtUsuarioMail.Size = new System.Drawing.Size(175, 20);
            this.txtUsuarioMail.TabIndex = 40;
            this.txtUsuarioMail.Tag = "Nombre de usuario de la cuenta de correo";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(4, 37);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(46, 13);
            this.label24.TabIndex = 15;
            this.label24.Text = "Usuario:";
            // 
            // txtPasswordMail
            // 
            this.txtPasswordMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasswordMail.Location = new System.Drawing.Point(392, 37);
            this.txtPasswordMail.Name = "txtPasswordMail";
            this.txtPasswordMail.Size = new System.Drawing.Size(175, 20);
            this.txtPasswordMail.TabIndex = 50;
            this.txtPasswordMail.Tag = "Contraseña de la cuenta de correo";
            this.txtPasswordMail.UseSystemPasswordChar = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(322, 40);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(64, 13);
            this.label25.TabIndex = 17;
            this.label25.Text = "Contraseña:";
            // 
            // chkAlertasViaEmailActivadas
            // 
            this.chkAlertasViaEmailActivadas.AutoSize = true;
            this.chkAlertasViaEmailActivadas.CausesValidation = false;
            this.chkAlertasViaEmailActivadas.Location = new System.Drawing.Point(8, 24);
            this.chkAlertasViaEmailActivadas.Name = "chkAlertasViaEmailActivadas";
            this.chkAlertasViaEmailActivadas.Size = new System.Drawing.Size(102, 17);
            this.chkAlertasViaEmailActivadas.TabIndex = 10;
            this.chkAlertasViaEmailActivadas.Tag = "";
            this.chkAlertasViaEmailActivadas.Text = "Envío activado:";
            this.chkAlertasViaEmailActivadas.UseVisualStyleBackColor = true;
            // 
            // gbxLogNivel
            // 
            this.gbxLogNivel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxLogNivel.Controls.Add(this.label37);
            this.gbxLogNivel.Controls.Add(this.btnVerLogs);
            this.gbxLogNivel.Controls.Add(this.label36);
            this.gbxLogNivel.Controls.Add(this.nudLogFilMaxSize);
            this.gbxLogNivel.Controls.Add(this.optLogNivelSoloErrores);
            this.gbxLogNivel.Controls.Add(this.txtRutaArchivoLog);
            this.gbxLogNivel.Controls.Add(this.optLogNivelDetallado);
            this.gbxLogNivel.Location = new System.Drawing.Point(5, 7);
            this.gbxLogNivel.Name = "gbxLogNivel";
            this.gbxLogNivel.Size = new System.Drawing.Size(858, 86);
            this.gbxLogNivel.TabIndex = 11;
            this.gbxLogNivel.TabStop = false;
            this.gbxLogNivel.Text = "Nivel de registro";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(406, 23);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(23, 13);
            this.label37.TabIndex = 105;
            this.label37.Text = "MB";
            // 
            // btnVerLogs
            // 
            this.btnVerLogs.Image = global::SincroStock.Administrador.Properties.Resources.logs;
            this.btnVerLogs.Location = new System.Drawing.Point(18, 44);
            this.btnVerLogs.Name = "btnVerLogs";
            this.btnVerLogs.Size = new System.Drawing.Size(81, 33);
            this.btnVerLogs.TabIndex = 40;
            this.btnVerLogs.Tag = "Abre el archivo log con la aplicación predeterminada para el tipo de archivo \".lo" +
    "g\"";
            this.btnVerLogs.Text = "Ver log";
            this.btnVerLogs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVerLogs.UseVisualStyleBackColor = true;
            this.btnVerLogs.Click += new System.EventHandler(this.btnVerLogs_Click);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(199, 23);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(143, 13);
            this.label36.TabIndex = 104;
            this.label36.Text = "Tamaño máximo por archivo:";
            // 
            // nudLogFilMaxSize
            // 
            this.nudLogFilMaxSize.Location = new System.Drawing.Point(344, 20);
            this.nudLogFilMaxSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudLogFilMaxSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLogFilMaxSize.Name = "nudLogFilMaxSize";
            this.nudLogFilMaxSize.Size = new System.Drawing.Size(59, 20);
            this.nudLogFilMaxSize.TabIndex = 30;
            this.nudLogFilMaxSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // optLogNivelSoloErrores
            // 
            this.optLogNivelSoloErrores.AutoSize = true;
            this.optLogNivelSoloErrores.Checked = true;
            this.optLogNivelSoloErrores.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optLogNivelSoloErrores.Location = new System.Drawing.Point(96, 21);
            this.optLogNivelSoloErrores.Name = "optLogNivelSoloErrores";
            this.optLogNivelSoloErrores.Size = new System.Drawing.Size(81, 17);
            this.optLogNivelSoloErrores.TabIndex = 20;
            this.optLogNivelSoloErrores.TabStop = true;
            this.optLogNivelSoloErrores.Tag = "Nivel de registro en el archivo log (detallado: registro de las operaciones paso " +
    "a paso, solo errores: graba solo los errores ocurridos junto con la pila de llam" +
    "adas)";
            this.optLogNivelSoloErrores.Text = "Solo errores";
            this.optLogNivelSoloErrores.UseVisualStyleBackColor = true;
            // 
            // txtRutaArchivoLog
            // 
            this.txtRutaArchivoLog.BackColor = System.Drawing.Color.OldLace;
            this.txtRutaArchivoLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaArchivoLog.Location = new System.Drawing.Point(105, 51);
            this.txtRutaArchivoLog.Name = "txtRutaArchivoLog";
            this.txtRutaArchivoLog.ReadOnly = true;
            this.txtRutaArchivoLog.Size = new System.Drawing.Size(747, 20);
            this.txtRutaArchivoLog.TabIndex = 50;
            this.txtRutaArchivoLog.Tag = "Dirección del servidor de correo (ej. smtp.miservidor.com.ar)";
            // 
            // optLogNivelDetallado
            // 
            this.optLogNivelDetallado.AutoSize = true;
            this.optLogNivelDetallado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optLogNivelDetallado.Location = new System.Drawing.Point(19, 21);
            this.optLogNivelDetallado.Name = "optLogNivelDetallado";
            this.optLogNivelDetallado.Size = new System.Drawing.Size(70, 17);
            this.optLogNivelDetallado.TabIndex = 10;
            this.optLogNivelDetallado.Tag = "Nivel de registro en el archivo log (detallado: registro de las operaciones paso " +
    "a paso, solo errores: graba solo los errores ocurridos junto con la pila de llam" +
    "adas)";
            this.optLogNivelDetallado.Text = "Detallado";
            this.optLogNivelDetallado.UseVisualStyleBackColor = true;
            // 
            // tabEjecucionPlanificada
            // 
            this.tabEjecucionPlanificada.Controls.Add(this.gbxOpcionesDelServicio);
            this.tabEjecucionPlanificada.Controls.Add(this.gbxFrecuenciaEjecucion);
            this.tabEjecucionPlanificada.Location = new System.Drawing.Point(4, 22);
            this.tabEjecucionPlanificada.Name = "tabEjecucionPlanificada";
            this.tabEjecucionPlanificada.Padding = new System.Windows.Forms.Padding(3);
            this.tabEjecucionPlanificada.Size = new System.Drawing.Size(869, 470);
            this.tabEjecucionPlanificada.TabIndex = 3;
            this.tabEjecucionPlanificada.Text = "Ejecución planificada";
            this.tabEjecucionPlanificada.UseVisualStyleBackColor = true;
            // 
            // gbxOpcionesDelServicio
            // 
            this.gbxOpcionesDelServicio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxOpcionesDelServicio.Controls.Add(this.txtEstadoServicio);
            this.gbxOpcionesDelServicio.Controls.Add(this.label5);
            this.gbxOpcionesDelServicio.Controls.Add(this.btnIniciarServicio);
            this.gbxOpcionesDelServicio.Controls.Add(this.btnDetenerServicio);
            this.gbxOpcionesDelServicio.Controls.Add(this.btnDesinstalarServicio);
            this.gbxOpcionesDelServicio.Controls.Add(this.btnInstalarServicio);
            this.gbxOpcionesDelServicio.Location = new System.Drawing.Point(10, 7);
            this.gbxOpcionesDelServicio.Name = "gbxOpcionesDelServicio";
            this.gbxOpcionesDelServicio.Size = new System.Drawing.Size(848, 103);
            this.gbxOpcionesDelServicio.TabIndex = 11;
            this.gbxOpcionesDelServicio.TabStop = false;
            this.gbxOpcionesDelServicio.Text = "Opciones del servicio";
            // 
            // txtEstadoServicio
            // 
            this.txtEstadoServicio.BackColor = System.Drawing.Color.OldLace;
            this.txtEstadoServicio.Location = new System.Drawing.Point(269, 46);
            this.txtEstadoServicio.Name = "txtEstadoServicio";
            this.txtEstadoServicio.ReadOnly = true;
            this.txtEstadoServicio.Size = new System.Drawing.Size(172, 20);
            this.txtEstadoServicio.TabIndex = 26;
            this.txtEstadoServicio.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(217, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Estado: ";
            // 
            // btnIniciarServicio
            // 
            this.btnIniciarServicio.Location = new System.Drawing.Point(21, 31);
            this.btnIniciarServicio.Name = "btnIniciarServicio";
            this.btnIniciarServicio.Size = new System.Drawing.Size(80, 23);
            this.btnIniciarServicio.TabIndex = 5;
            this.btnIniciarServicio.Tag = "Inicia el servicio de Windows";
            this.btnIniciarServicio.Text = "Iniciar";
            this.btnIniciarServicio.UseVisualStyleBackColor = true;
            this.btnIniciarServicio.Click += new System.EventHandler(this.btnIniciarServicio_Click);
            // 
            // btnDetenerServicio
            // 
            this.btnDetenerServicio.Location = new System.Drawing.Point(107, 31);
            this.btnDetenerServicio.Name = "btnDetenerServicio";
            this.btnDetenerServicio.Size = new System.Drawing.Size(75, 23);
            this.btnDetenerServicio.TabIndex = 11;
            this.btnDetenerServicio.Tag = "Detiene el servicio de Windows";
            this.btnDetenerServicio.Text = "Detener";
            this.btnDetenerServicio.UseVisualStyleBackColor = true;
            this.btnDetenerServicio.Click += new System.EventHandler(this.btnDetenerServicio_Click);
            // 
            // btnDesinstalarServicio
            // 
            this.btnDesinstalarServicio.Location = new System.Drawing.Point(107, 60);
            this.btnDesinstalarServicio.Name = "btnDesinstalarServicio";
            this.btnDesinstalarServicio.Size = new System.Drawing.Size(75, 23);
            this.btnDesinstalarServicio.TabIndex = 21;
            this.btnDesinstalarServicio.Tag = "Desinstala el servicio de Windows";
            this.btnDesinstalarServicio.Text = "Desinstalar Servicio";
            this.btnDesinstalarServicio.UseVisualStyleBackColor = true;
            this.btnDesinstalarServicio.Click += new System.EventHandler(this.btnDesinstalarServicio_Click);
            // 
            // btnInstalarServicio
            // 
            this.btnInstalarServicio.Location = new System.Drawing.Point(21, 60);
            this.btnInstalarServicio.Name = "btnInstalarServicio";
            this.btnInstalarServicio.Size = new System.Drawing.Size(80, 23);
            this.btnInstalarServicio.TabIndex = 16;
            this.btnInstalarServicio.Tag = "Instala el servicio de Windows encargado de ejecutar automáticamente el proceso d" +
    "e carga según la planificación definida";
            this.btnInstalarServicio.Text = "Instalar";
            this.btnInstalarServicio.UseVisualStyleBackColor = true;
            this.btnInstalarServicio.Click += new System.EventHandler(this.btnInstalarServicio_Click);
            // 
            // gbxFrecuenciaEjecucion
            // 
            this.gbxFrecuenciaEjecucion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxFrecuenciaEjecucion.Controls.Add(this.panel2);
            this.gbxFrecuenciaEjecucion.Controls.Add(this.pnlEjecucionActivadaDesactivada);
            this.gbxFrecuenciaEjecucion.Location = new System.Drawing.Point(10, 114);
            this.gbxFrecuenciaEjecucion.Name = "gbxFrecuenciaEjecucion";
            this.gbxFrecuenciaEjecucion.Size = new System.Drawing.Size(848, 159);
            this.gbxFrecuenciaEjecucion.TabIndex = 21;
            this.gbxFrecuenciaEjecucion.TabStop = false;
            this.gbxFrecuenciaEjecucion.Text = "Frecuencia de ejecución de la tarea";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.dtpHoraHasta);
            this.panel2.Controls.Add(this.dtpHoraDesde);
            this.panel2.Controls.Add(this.dtpHora);
            this.panel2.Controls.Add(this.label42);
            this.panel2.Controls.Add(this.label39);
            this.panel2.Controls.Add(this.nupFrecAct);
            this.panel2.Controls.Add(this.cmbUnidadFrecAct);
            this.panel2.Controls.Add(this.label43);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(6, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(529, 100);
            this.panel2.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.optFrecEjecucionHoraEspecifica);
            this.panel4.Controls.Add(this.optFrecEjecucionCadaXUnidades);
            this.panel4.Location = new System.Drawing.Point(15, 36);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(46, 49);
            this.panel4.TabIndex = 72;
            // 
            // optFrecEjecucionHoraEspecifica
            // 
            this.optFrecEjecucionHoraEspecifica.AutoSize = true;
            this.optFrecEjecucionHoraEspecifica.Location = new System.Drawing.Point(0, 1);
            this.optFrecEjecucionHoraEspecifica.Name = "optFrecEjecucionHoraEspecifica";
            this.optFrecEjecucionHoraEspecifica.Size = new System.Drawing.Size(48, 17);
            this.optFrecEjecucionHoraEspecifica.TabIndex = 17;
            this.optFrecEjecucionHoraEspecifica.TabStop = true;
            this.optFrecEjecucionHoraEspecifica.Tag = "Programa la tarea para ejecutarse a una hora específica";
            this.optFrecEjecucionHoraEspecifica.Text = "A las";
            this.optFrecEjecucionHoraEspecifica.UseVisualStyleBackColor = true;
            this.optFrecEjecucionHoraEspecifica.CheckedChanged += new System.EventHandler(this.optFrecEjecucionHoraEspecifica_CheckedChanged);
            // 
            // optFrecEjecucionCadaXUnidades
            // 
            this.optFrecEjecucionCadaXUnidades.AutoSize = true;
            this.optFrecEjecucionCadaXUnidades.Location = new System.Drawing.Point(0, 30);
            this.optFrecEjecucionCadaXUnidades.Name = "optFrecEjecucionCadaXUnidades";
            this.optFrecEjecucionCadaXUnidades.Size = new System.Drawing.Size(50, 17);
            this.optFrecEjecucionCadaXUnidades.TabIndex = 17;
            this.optFrecEjecucionCadaXUnidades.TabStop = true;
            this.optFrecEjecucionCadaXUnidades.Tag = "Programa la tarea para ejecutarse cada x unidades de tiempo (minutos, horas)";
            this.optFrecEjecucionCadaXUnidades.Text = "Cada";
            this.optFrecEjecucionCadaXUnidades.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkMartes);
            this.panel3.Controls.Add(this.chkMiercoles);
            this.panel3.Controls.Add(this.chkDomingo);
            this.panel3.Controls.Add(this.chkJueves);
            this.panel3.Controls.Add(this.chkSabado);
            this.panel3.Controls.Add(this.chkViernes);
            this.panel3.Controls.Add(this.chkLunes);
            this.panel3.Location = new System.Drawing.Point(15, 9);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(505, 15);
            this.panel3.TabIndex = 17;
            // 
            // chkMartes
            // 
            this.chkMartes.AutoSize = true;
            this.chkMartes.Location = new System.Drawing.Point(65, 0);
            this.chkMartes.Name = "chkMartes";
            this.chkMartes.Size = new System.Drawing.Size(58, 17);
            this.chkMartes.TabIndex = 11;
            this.chkMartes.Tag = "Programa la tarea para ejecutarse los días Martes";
            this.chkMartes.Text = "Martes";
            this.chkMartes.UseVisualStyleBackColor = true;
            this.chkMartes.CheckedChanged += new System.EventHandler(this.chkDIa_CheckedChanged);
            // 
            // chkMiercoles
            // 
            this.chkMiercoles.AutoSize = true;
            this.chkMiercoles.Location = new System.Drawing.Point(132, 0);
            this.chkMiercoles.Name = "chkMiercoles";
            this.chkMiercoles.Size = new System.Drawing.Size(71, 17);
            this.chkMiercoles.TabIndex = 16;
            this.chkMiercoles.Tag = "Programa la tarea para ejecutarse los días Miércoles";
            this.chkMiercoles.Text = "Miércoles";
            this.chkMiercoles.UseVisualStyleBackColor = true;
            this.chkMiercoles.CheckedChanged += new System.EventHandler(this.chkDIa_CheckedChanged);
            // 
            // chkDomingo
            // 
            this.chkDomingo.AutoSize = true;
            this.chkDomingo.Location = new System.Drawing.Point(423, 0);
            this.chkDomingo.Name = "chkDomingo";
            this.chkDomingo.Size = new System.Drawing.Size(68, 17);
            this.chkDomingo.TabIndex = 36;
            this.chkDomingo.Tag = "Programa la tarea para ejecutarse los días Domingo";
            this.chkDomingo.Text = "Domingo";
            this.chkDomingo.UseVisualStyleBackColor = true;
            this.chkDomingo.CheckedChanged += new System.EventHandler(this.chkDIa_CheckedChanged);
            // 
            // chkJueves
            // 
            this.chkJueves.AutoSize = true;
            this.chkJueves.Location = new System.Drawing.Point(212, 0);
            this.chkJueves.Name = "chkJueves";
            this.chkJueves.Size = new System.Drawing.Size(60, 17);
            this.chkJueves.TabIndex = 21;
            this.chkJueves.Tag = "Programa la tarea para ejecutarse los días Jueves";
            this.chkJueves.Text = "Jueves";
            this.chkJueves.UseVisualStyleBackColor = true;
            this.chkJueves.CheckedChanged += new System.EventHandler(this.chkDIa_CheckedChanged);
            // 
            // chkSabado
            // 
            this.chkSabado.AutoSize = true;
            this.chkSabado.Location = new System.Drawing.Point(351, 0);
            this.chkSabado.Name = "chkSabado";
            this.chkSabado.Size = new System.Drawing.Size(63, 17);
            this.chkSabado.TabIndex = 31;
            this.chkSabado.Tag = "Programa la tarea para ejecutarse los días Sábado";
            this.chkSabado.Text = "Sábado";
            this.chkSabado.UseVisualStyleBackColor = true;
            this.chkSabado.CheckedChanged += new System.EventHandler(this.chkDIa_CheckedChanged);
            // 
            // chkViernes
            // 
            this.chkViernes.AutoSize = true;
            this.chkViernes.Location = new System.Drawing.Point(281, 0);
            this.chkViernes.Name = "chkViernes";
            this.chkViernes.Size = new System.Drawing.Size(61, 17);
            this.chkViernes.TabIndex = 26;
            this.chkViernes.Tag = "Programa la tarea para ejecutarse los días Viernes";
            this.chkViernes.Text = "Viernes";
            this.chkViernes.UseVisualStyleBackColor = true;
            this.chkViernes.CheckedChanged += new System.EventHandler(this.chkDIa_CheckedChanged);
            // 
            // chkLunes
            // 
            this.chkLunes.AutoSize = true;
            this.chkLunes.Location = new System.Drawing.Point(1, 0);
            this.chkLunes.Name = "chkLunes";
            this.chkLunes.Size = new System.Drawing.Size(55, 17);
            this.chkLunes.TabIndex = 5;
            this.chkLunes.Tag = "Programa la tarea para ejecutarse los días Lunes";
            this.chkLunes.Text = "Lunes";
            this.chkLunes.UseVisualStyleBackColor = true;
            this.chkLunes.CheckedChanged += new System.EventHandler(this.chkDIa_CheckedChanged);
            // 
            // dtpHoraHasta
            // 
            this.dtpHoraHasta.CustomFormat = "HH:mm";
            this.dtpHoraHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHoraHasta.Location = new System.Drawing.Point(369, 65);
            this.dtpHoraHasta.Name = "dtpHoraHasta";
            this.dtpHoraHasta.ShowUpDown = true;
            this.dtpHoraHasta.Size = new System.Drawing.Size(58, 20);
            this.dtpHoraHasta.TabIndex = 71;
            this.dtpHoraHasta.Value = new System.DateTime(2016, 11, 29, 23, 59, 59, 0);
            this.dtpHoraHasta.Validating += new System.ComponentModel.CancelEventHandler(this.dtpHoraHasta_Validating);
            // 
            // dtpHoraDesde
            // 
            this.dtpHoraDesde.CustomFormat = "HH:mm";
            this.dtpHoraDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHoraDesde.Location = new System.Drawing.Point(277, 65);
            this.dtpHoraDesde.Name = "dtpHoraDesde";
            this.dtpHoraDesde.ShowUpDown = true;
            this.dtpHoraDesde.Size = new System.Drawing.Size(58, 20);
            this.dtpHoraDesde.TabIndex = 66;
            this.dtpHoraDesde.Value = new System.DateTime(2016, 11, 29, 0, 0, 0, 0);
            this.dtpHoraDesde.Validating += new System.ComponentModel.CancelEventHandler(this.dtpHoraDesde_Validating);
            // 
            // dtpHora
            // 
            this.dtpHora.CustomFormat = "HH:mm";
            this.dtpHora.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHora.Location = new System.Drawing.Point(66, 35);
            this.dtpHora.Name = "dtpHora";
            this.dtpHora.ShowUpDown = true;
            this.dtpHora.Size = new System.Drawing.Size(58, 20);
            this.dtpHora.TabIndex = 46;
            this.dtpHora.Tag = "Programa la tarea para ejecutarse a una hora específica";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(338, 69);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(28, 13);
            this.label42.TabIndex = 0;
            this.label42.Text = "y las";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(230, 68);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(50, 13);
            this.label39.TabIndex = 0;
            this.label39.Text = "entre las ";
            // 
            // nupFrecAct
            // 
            this.nupFrecAct.Location = new System.Drawing.Point(66, 65);
            this.nupFrecAct.Maximum = new decimal(new int[] {
            20000000,
            0,
            0,
            0});
            this.nupFrecAct.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupFrecAct.Name = "nupFrecAct";
            this.nupFrecAct.Size = new System.Drawing.Size(68, 20);
            this.nupFrecAct.TabIndex = 56;
            this.nupFrecAct.Tag = "Programa la tarea para ejecutarse cada x unidades de tiempo (minutos, horas)";
            this.nupFrecAct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nupFrecAct.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbUnidadFrecAct
            // 
            this.cmbUnidadFrecAct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnidadFrecAct.FormattingEnabled = true;
            this.cmbUnidadFrecAct.Location = new System.Drawing.Point(142, 65);
            this.cmbUnidadFrecAct.Name = "cmbUnidadFrecAct";
            this.cmbUnidadFrecAct.Size = new System.Drawing.Size(82, 21);
            this.cmbUnidadFrecAct.TabIndex = 61;
            this.cmbUnidadFrecAct.Tag = "Programa la tarea para ejecutarse cada x unidades de tiempo (minutos, horas)";
            this.cmbUnidadFrecAct.SelectedIndexChanged += new System.EventHandler(this.cmbUnidadFrecAct_SelectedIndexChanged);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(433, 69);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(21, 13);
            this.label43.TabIndex = 0;
            this.label43.Text = "hs.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "hs.";
            // 
            // pnlEjecucionActivadaDesactivada
            // 
            this.pnlEjecucionActivadaDesactivada.BackColor = System.Drawing.Color.Honeydew;
            this.pnlEjecucionActivadaDesactivada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEjecucionActivadaDesactivada.Controls.Add(this.chkEjecucionActivadaDesactivada);
            this.pnlEjecucionActivadaDesactivada.Location = new System.Drawing.Point(548, 70);
            this.pnlEjecucionActivadaDesactivada.Name = "pnlEjecucionActivadaDesactivada";
            this.pnlEjecucionActivadaDesactivada.Size = new System.Drawing.Size(89, 26);
            this.pnlEjecucionActivadaDesactivada.TabIndex = 11;
            this.pnlEjecucionActivadaDesactivada.Tag = "Habilita o deshabilita la ejecución automática de la interfaz según la planificac" +
    "ión definida";
            // 
            // chkEjecucionActivadaDesactivada
            // 
            this.chkEjecucionActivadaDesactivada.AutoSize = true;
            this.chkEjecucionActivadaDesactivada.Checked = true;
            this.chkEjecucionActivadaDesactivada.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEjecucionActivadaDesactivada.Location = new System.Drawing.Point(18, 4);
            this.chkEjecucionActivadaDesactivada.Name = "chkEjecucionActivadaDesactivada";
            this.chkEjecucionActivadaDesactivada.Size = new System.Drawing.Size(68, 17);
            this.chkEjecucionActivadaDesactivada.TabIndex = 6;
            this.chkEjecucionActivadaDesactivada.Tag = "Habilita o deshabilita la ejecución automática de la interfaz según la planificac" +
    "ión definida";
            this.chkEjecucionActivadaDesactivada.Text = "Activada";
            this.chkEjecucionActivadaDesactivada.UseVisualStyleBackColor = true;
            this.chkEjecucionActivadaDesactivada.CheckedChanged += new System.EventHandler(this.chkEjecucionActivadaDesactivada_CheckedChanged);
            // 
            // tabEjecucionManual
            // 
            this.tabEjecucionManual.Controls.Add(this.pgbEjecucionManualProgresoEtapa);
            this.tabEjecucionManual.Controls.Add(this.pgbEjecucionManualProgresoTotal);
            this.tabEjecucionManual.Controls.Add(this.lblEjecucionManualDescripcionPasoEtapa);
            this.tabEjecucionManual.Controls.Add(this.lblEjecucionManualDescripcionEtapa);
            this.tabEjecucionManual.Controls.Add(this.label40);
            this.tabEjecucionManual.Controls.Add(this.label48);
            this.tabEjecucionManual.Controls.Add(this.panel1);
            this.tabEjecucionManual.Controls.Add(this.label38);
            this.tabEjecucionManual.Controls.Add(this.btnEjecutarProceso);
            this.tabEjecucionManual.Location = new System.Drawing.Point(4, 22);
            this.tabEjecucionManual.Name = "tabEjecucionManual";
            this.tabEjecucionManual.Padding = new System.Windows.Forms.Padding(3);
            this.tabEjecucionManual.Size = new System.Drawing.Size(869, 470);
            this.tabEjecucionManual.TabIndex = 5;
            this.tabEjecucionManual.Text = "Ejecución Manual";
            this.tabEjecucionManual.UseVisualStyleBackColor = true;
            // 
            // pgbEjecucionManualProgresoEtapa
            // 
            this.pgbEjecucionManualProgresoEtapa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgbEjecucionManualProgresoEtapa.Location = new System.Drawing.Point(125, 400);
            this.pgbEjecucionManualProgresoEtapa.Name = "pgbEjecucionManualProgresoEtapa";
            this.pgbEjecucionManualProgresoEtapa.Size = new System.Drawing.Size(710, 15);
            this.pgbEjecucionManualProgresoEtapa.TabIndex = 4;
            // 
            // pgbEjecucionManualProgresoTotal
            // 
            this.pgbEjecucionManualProgresoTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgbEjecucionManualProgresoTotal.Location = new System.Drawing.Point(125, 364);
            this.pgbEjecucionManualProgresoTotal.Name = "pgbEjecucionManualProgresoTotal";
            this.pgbEjecucionManualProgresoTotal.Size = new System.Drawing.Size(710, 15);
            this.pgbEjecucionManualProgresoTotal.TabIndex = 4;
            // 
            // lblEjecucionManualDescripcionPasoEtapa
            // 
            this.lblEjecucionManualDescripcionPasoEtapa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEjecucionManualDescripcionPasoEtapa.AutoSize = true;
            this.lblEjecucionManualDescripcionPasoEtapa.ForeColor = System.Drawing.Color.Black;
            this.lblEjecucionManualDescripcionPasoEtapa.Location = new System.Drawing.Point(125, 418);
            this.lblEjecucionManualDescripcionPasoEtapa.Name = "lblEjecucionManualDescripcionPasoEtapa";
            this.lblEjecucionManualDescripcionPasoEtapa.Size = new System.Drawing.Size(0, 13);
            this.lblEjecucionManualDescripcionPasoEtapa.TabIndex = 6;
            this.lblEjecucionManualDescripcionPasoEtapa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEjecucionManualDescripcionEtapa
            // 
            this.lblEjecucionManualDescripcionEtapa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEjecucionManualDescripcionEtapa.AutoSize = true;
            this.lblEjecucionManualDescripcionEtapa.ForeColor = System.Drawing.Color.Black;
            this.lblEjecucionManualDescripcionEtapa.Location = new System.Drawing.Point(125, 382);
            this.lblEjecucionManualDescripcionEtapa.Name = "lblEjecucionManualDescripcionEtapa";
            this.lblEjecucionManualDescripcionEtapa.Size = new System.Drawing.Size(0, 13);
            this.lblEjecucionManualDescripcionEtapa.TabIndex = 6;
            this.lblEjecucionManualDescripcionEtapa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label40
            // 
            this.label40.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(29, 402);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(82, 13);
            this.label40.TabIndex = 5;
            this.label40.Text = "Progreso etapa:";
            // 
            // label48
            // 
            this.label48.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(29, 365);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(90, 13);
            this.label48.TabIndex = 5;
            this.label48.Text = "Progreso general:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtResultadoProceso);
            this.panel1.Location = new System.Drawing.Point(32, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 328);
            this.panel1.TabIndex = 3;
            // 
            // txtResultadoProceso
            // 
            this.txtResultadoProceso.BackColor = System.Drawing.Color.OldLace;
            this.txtResultadoProceso.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResultadoProceso.BulletIndent = 1;
            this.txtResultadoProceso.DetectUrls = false;
            this.txtResultadoProceso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResultadoProceso.Location = new System.Drawing.Point(0, 0);
            this.txtResultadoProceso.Name = "txtResultadoProceso";
            this.txtResultadoProceso.ReadOnly = true;
            this.txtResultadoProceso.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtResultadoProceso.Size = new System.Drawing.Size(802, 326);
            this.txtResultadoProceso.TabIndex = 6;
            this.txtResultadoProceso.Text = "";
            this.txtResultadoProceso.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtResultadoProceso_MouseDown);
            // 
            // label38
            // 
            this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(30, 13);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(109, 13);
            this.label38.TabIndex = 2;
            this.label38.Text = "Informe de ejecución:";
            // 
            // btnEjecutarProceso
            // 
            this.btnEjecutarProceso.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEjecutarProceso.Image = global::SincroStock.Administrador.Properties.Resources.start;
            this.btnEjecutarProceso.Location = new System.Drawing.Point(382, 428);
            this.btnEjecutarProceso.Name = "btnEjecutarProceso";
            this.btnEjecutarProceso.Size = new System.Drawing.Size(104, 32);
            this.btnEjecutarProceso.TabIndex = 11;
            this.btnEjecutarProceso.Tag = "Ejecución del proceso de carga de la interfaz";
            this.btnEjecutarProceso.Text = "&Iniciar";
            this.btnEjecutarProceso.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEjecutarProceso.UseVisualStyleBackColor = true;
            this.btnEjecutarProceso.Click += new System.EventHandler(this.btnEjecutarProceso_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xls";
            this.openFileDialog.Filter = "Archivos de Microsoft Excel|*.xls";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // bgwEjecutarProcesoCarga
            // 
            this.bgwEjecutarProcesoCarga.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwEjecutarProcesoCarga_DoWork);
            this.bgwEjecutarProcesoCarga.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwEjecutarProcesoCarga_RunWorkerCompleted);
            // 
            // timerRefreshServiceStatus
            // 
            this.timerRefreshServiceStatus.Interval = 2000;
            this.timerRefreshServiceStatus.Tick += new System.EventHandler(this.timerRefreshServiceStatus_Tick);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Title = "Guardar como";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsSLToolTipText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 551);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(936, 22);
            this.statusStrip1.TabIndex = 31;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // tsSLToolTipText
            // 
            this.tsSLToolTipText.BackColor = System.Drawing.Color.Transparent;
            this.tsSLToolTipText.Name = "tsSLToolTipText";
            this.tsSLToolTipText.Size = new System.Drawing.Size(921, 17);
            this.tsSLToolTipText.Spring = true;
            this.tsSLToolTipText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.BackColor = System.Drawing.SystemColors.Control;
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::SincroStock.Administrador.Properties.Resources.shutdown;
            this.btnSalir.Location = new System.Drawing.Point(829, 514);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(78, 33);
            this.btnSalir.TabIndex = 31;
            this.btnSalir.Tag = "Cerrar el administrador";
            this.btnSalir.Text = "&Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.SystemColors.Control;
            this.btnGuardar.Image = global::SincroStock.Administrador.Properties.Resources.save;
            this.btnGuardar.Location = new System.Drawing.Point(745, 514);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(78, 33);
            this.btnGuardar.TabIndex = 21;
            this.btnGuardar.Tag = "Guardar configuración";
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // FormAdministrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(936, 573);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControlPrincipal);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnSalir);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 547);
            this.Name = "FormAdministrador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAdministrador_FormClosing);
            this.Load += new System.EventHandler(this.FormAdministrador_Load);
            this.gbxConexionTangoBDOrigen.ResumeLayout(false);
            this.gbxConexionTangoBDOrigen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeoutSqlCommandOrigen)).EndInit();
            this.tabControlPrincipal.ResumeLayout(false);
            this.tabConexionTango.ResumeLayout(false);
            this.gbxConexionTangoBDDestino.ResumeLayout(false);
            this.gbxConexionTangoBDDestino.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeoutSqlCommandDestino)).EndInit();
            this.tabParametrosSincro.ResumeLayout(false);
            this.tblParametrosSincronizacion.ResumeLayout(false);
            this.tblParametrosSincronizacion.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudAntiguedadMaximaMovimientos)).EndInit();
            this.tabRegistroDeActividad.ResumeLayout(false);
            this.gbxAlertasViaEmail.ResumeLayout(false);
            this.gbxAlertasViaEmail.PerformLayout();
            this.pnlDatosAlertaEmail.ResumeLayout(false);
            this.pnlDatosAlertaEmail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlertasErrorMailFlushInterval)).EndInit();
            this.gbxLogNivel.ResumeLayout(false);
            this.gbxLogNivel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogFilMaxSize)).EndInit();
            this.tabEjecucionPlanificada.ResumeLayout(false);
            this.gbxOpcionesDelServicio.ResumeLayout(false);
            this.gbxOpcionesDelServicio.PerformLayout();
            this.gbxFrecuenciaEjecucion.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupFrecAct)).EndInit();
            this.pnlEjecucionActivadaDesactivada.ResumeLayout(false);
            this.pnlEjecucionActivadaDesactivada.PerformLayout();
            this.tabEjecucionManual.ResumeLayout(false);
            this.tabEjecucionManual.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServidorSQLOrigen;
        private System.Windows.Forms.TextBox txtUsuarioSQLOrigen;
        private System.Windows.Forms.TextBox txtPasswordSQLOrigen;
        private System.Windows.Forms.Label lblServerSQL;
        private System.Windows.Forms.Label lblNombreUsr;
        private System.Windows.Forms.Label lblContrasenia;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox gbxConexionTangoBDOrigen;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TabPage tabConexionTango;
        private System.Windows.Forms.TabPage tabEjecucionPlanificada;
        private System.Windows.Forms.TextBox txtNombreBDOrigen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.NumericUpDown nupFrecAct;
        private System.Windows.Forms.ComboBox cmbUnidadFrecAct;
        private System.Windows.Forms.GroupBox gbxFrecuenciaEjecucion;
        private System.Windows.Forms.GroupBox gbxOpcionesDelServicio;
        private System.Windows.Forms.TextBox txtEstadoServicio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnIniciarServicio;
        private System.Windows.Forms.Button btnDetenerServicio;
        private System.Windows.Forms.Button btnDesinstalarServicio;
        private System.Windows.Forms.Button btnInstalarServicio;
        private System.Windows.Forms.CheckBox chkMiercoles;
        private System.Windows.Forms.CheckBox chkDomingo;
        private System.Windows.Forms.CheckBox chkSabado;
        private System.Windows.Forms.CheckBox chkViernes;
        private System.Windows.Forms.CheckBox chkJueves;
        private System.Windows.Forms.CheckBox chkLunes;
        private System.Windows.Forms.CheckBox chkMartes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TabPage tabEjecucionManual;
        private System.Windows.Forms.Button btnEjecutarProceso;
        private System.ComponentModel.BackgroundWorker bgwEjecutarProcesoCarga;
        private System.Windows.Forms.Button btnProbarConexionTango;
        private System.Windows.Forms.TabPage tabRegistroDeActividad;
        private System.Windows.Forms.GroupBox gbxAlertasViaEmail;
        private System.Windows.Forms.GroupBox gbxLogNivel;
        private System.Windows.Forms.RadioButton optLogNivelSoloErrores;
        private System.Windows.Forms.RadioButton optLogNivelDetallado;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel pnlDatosAlertaEmail;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtServidorMail;
        private System.Windows.Forms.Button btnProbarEnvioMail;
        private System.Windows.Forms.TextBox txtPuertoMail;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtDireccionMail;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtUsuarioMail;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtPasswordMail;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel pnlEjecucionActivadaDesactivada;
        private System.Windows.Forms.CheckBox chkEjecucionActivadaDesactivada;
        private System.Windows.Forms.TabControl tabControlPrincipal;
        private System.Windows.Forms.RichTextBox txtResultadoProceso;
        private System.Windows.Forms.CheckBox chkSSL;
        private System.Windows.Forms.Timer timerRefreshServiceStatus;
        private System.Windows.Forms.Button btnVerLogs;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtpHoraHasta;
        private System.Windows.Forms.DateTimePicker dtpHoraDesde;
        private System.Windows.Forms.DateTimePicker dtpHora;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.RadioButton optFrecEjecucionCadaXUnidades;
        private System.Windows.Forms.RadioButton optFrecEjecucionHoraEspecifica;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.ProgressBar pgbEjecucionManualProgresoTotal;
        private System.Windows.Forms.ProgressBar pgbEjecucionManualProgresoEtapa;
        private System.Windows.Forms.Label lblEjecucionManualDescripcionEtapa;
        private System.Windows.Forms.Label lblEjecucionManualDescripcionPasoEtapa;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsSLToolTipText;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabPage tabParametrosSincro;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.NumericUpDown nudTimeoutSqlCommandOrigen;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtDestinatariosAlertas;
        private System.Windows.Forms.CheckBox chkAlertasViaEmailActivadas;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.NumericUpDown nudLogFilMaxSize;
        private System.Windows.Forms.NumericUpDown nudAlertasErrorMailFlushInterval;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.TableLayoutPanel tblParametrosSincronizacion;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtRutaArchivoLog;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox gbxConexionTangoBDDestino;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txtNombreBDDestino;
        private System.Windows.Forms.NumericUpDown nudTimeoutSqlCommandDestino;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox txtPasswordSQLDestino;
        private System.Windows.Forms.TextBox txtUsuarioSQLDestino;
        private System.Windows.Forms.TextBox txtServidorSQLDestino;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTipoComprobanteAjuste;
        private System.Windows.Forms.NumericUpDown nudAntiguedadMaximaMovimientos;
    }
}