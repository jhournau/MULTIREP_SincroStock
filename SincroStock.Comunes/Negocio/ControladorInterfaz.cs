using GC.Utils.Helpers;
using SincroStock.Comunes.Datos.Tango.DAO;
using SincroStock.Comunes.Exceptions;
using SincroStock.Comunes.Utils;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SincroStock.Comunes.Negocio
{
    public enum ModoEjecucion
    {
        GUI,
        SERVICIO,
        INDETERMINADO
    }

    public class ControladorInterfaz
    {
        private static object InstanceLockObject = new object();
        private static ControladorInterfaz instance = null;
        public ModoEjecucion Modo { get; set; }
        private ConfigGeneral config;
        private static ILog logger = LogManager.GetLogger(typeof(ControladorInterfaz));
        private bool interrumpirProceso;
        private CancellationToken cancellationToken = new CancellationToken();

        public bool InterrumpirProceso
        {
            get { return this.interrumpirProceso; }
            set
            {
                this.interrumpirProceso = value;
                cancellationToken.IsCancellationRequested = value;
            }
        }
        
        public bool ProcesoEnEjecucion
        {
            get
            {
                bool mutexLibre;
                try
                {
                    using (Mutex lockProceso = new Mutex(true, ConfigGeneral.Instance.GUID, out mutexLibre))
                    {
                        return !mutexLibre;
                    }
                }
                catch (Exception)
                {
                    return true;
                }
            }
        }

        private ControladorInterfaz()
        {
            this.config = ConfigGeneral.Instance;
            this.Modo = ModoEjecucion.INDETERMINADO;
        }

        public static ControladorInterfaz Instance
        {
            get
            {
                lock (ControladorInterfaz.InstanceLockObject)
                {
                    if (ControladorInterfaz.instance == null)
                        ControladorInterfaz.instance = new ControladorInterfaz();
                    return ControladorInterfaz.instance;
                }
            }
        }

        //Método principal de la aplicación, es el que dispara la acción cuando el servicio
        //detecta que debe ejecutar la tarea de carga o se utiliza la opción manual de la GUI
        public void iniciarTarea()
        {
            try
            {
                LogUtil.Log(logger, Level.Debug, nameof(iniciarTarea) + " invocado desde " + new GC.Utils.AssemblyInfo(Assembly.GetCallingAssembly()).Product);
                LogUtil.LogWithGui(logger, Level.Info, "Tarea inciada", true);

                if (this.Modo == ModoEjecucion.INDETERMINADO)
                    throw new Exception("No se especificó el modo de ejecución");

                //this.validarVersionTango();

                string msjInfo;

                config = ConfigGeneral.Instance;

                bool mutexLibre;
                using (Mutex lockProceso = new Mutex(true, config.GUID, out mutexLibre))
                {

                    if (!mutexLibre)
                    {
                        msjInfo = "El proceso se encuentra en ejecución desde otra instancia, debe esperar a que finalice.";
                        LogUtil.Log(logger, Level.Debug, msjInfo);
                        throw new BusyProcessException(msjInfo);
                    }

                    string msjErrorValidacionConfig = config.Error;
                    if (!String.IsNullOrEmpty(msjErrorValidacionConfig))
                        throw new Exception("Error de validación configuración. " + msjErrorValidacionConfig);

                    int cantidadEtapas = 2;

                    if (OnComienzoProceso != null)
                        this.OnComienzoProceso(cantidadEtapas);

                    //******** PARA TESTING
                    //Thread.Sleep(30000);
                    //LogUtil.Log(_logger, Level.Error, "PRUEBA ERROR ", true, true, true, true, null);
                    //verificarInterrupcion();
                    //********

                    //if (this.config.TareaFacturacionActivada)
                        this.sincronizarStock();
                    //else
                      //  LogUtil.LogWithGui(logger, Level.Debug, "La carga de facturas no será ejecutada porque se encuentra desactivada en la configuración", true);
                }
            }
            finally
            {
                LogUtil.Log(logger, Level.Debug, nameof(iniciarTarea) + " finalizado");
                //System.GC.Collect();

                if (OnFinProceso != null)
                    this.OnFinProceso();

            }
        }

        private void sincronizarStock()
        {
            string mensajeError = null;
            try
            {
                verificarInterrupcion();

                string msjInfo;

                var sincronizadorStock = new SincronizadorStock();

                msjInfo = "Obteniendo movimientos de stock pendientes de sincronizar";
                LogUtil.LogWithGui(logger, Level.Info, msjInfo, true);

                if (this.OnCambioEtapa != null)
                    this.OnCambioEtapa(msjInfo, 1);

                var cantMovimientos = sincronizadorStock.PrepararMovimientos(this.cancellationToken);
                if (this.OnPasoEnEtapa != null)
                    this.OnPasoEnEtapa("");
               
                if (cantMovimientos > 0)
                {
                    verificarInterrupcion();

                    msjInfo = "Sincronizando movimientos";
                    LogUtil.LogWithGui(logger, Level.Info, msjInfo, true);
                    if (this.OnCambioEtapa != null)
                        this.OnCambioEtapa(msjInfo, cantMovimientos);

                    sincronizadorStock.SincronizarMovimientos(cancellationToken, 
                        (sincroMov) => this.OnPasoEnEtapa?.Invoke(
                            $"{sincroMov.SincroMovimientoOrigenDTO.ComprobanteStock.TCOMP_IN_S} {sincroMov.SincroMovimientoOrigenDTO.ComprobanteStock.NCOMP_IN_S}"));

                }
                else
                {
                    LogUtil.LogWithGui(logger, Level.Info, "No se encontraron movimientos para procesar", true);
                }

                if (this.Modo == ModoEjecucion.SERVICIO)
                {
                    sincronizadorStock.NotificarErroneos(cancellationToken);
                }
            }
            catch (UserAbortException uae)
            {
                throw uae;
            }
            catch (Exception ex)
            {
                LogUtil.Log(logger, Level.Error, ex.Message, true, true, ex);
            }
        }


        #region Métodos auxiliares

        public void verificarInterrupcion()
        {
            if (InterrumpirProceso)
            {
                InterrumpirProceso = false;
                throw new UserAbortException("Proceso cancelado");
            }
        }


        //internal void validarVersionTango()
        //{

        //    LogUtil.Log(logger, Level.Debug, "Validando versión de Tango");
        //    LogUtil.Log(logger, Level.Debug, "Obteniendo versión");
        //    string versionTangoStr = GC.Tango.Common.Datos.DAO.EmpresaDAO.GetVersionTango(config.TangoDBConnectionString);
        //    int versionTango;
        //    if (!int.TryParse(versionTangoStr, out versionTango))
        //        throw new Exception($"No se pudo convertir a entero el valor obtenido para la versión de Tango de la base de datos ({versionTangoStr})");
        //    LogUtil.Log(logger, Level.Debug, "Versión: " + versionTango);

        //    if(versionTango != versionTangoSoportada)
        //        throw new Exception($"La versión de Tango detectada ({versionTango}) no se corresponde con la soportada ({versionTangoSoportada})");

        //}

        #endregion Métodos auxiliares

        #region Delegados y eventos

        public delegate void OnComienzoProcesoEventHandler(int cantidadDeEtapas);
        public event OnComienzoProcesoEventHandler OnComienzoProceso;

        public delegate void OnFinProcesoEventHandler();
        public event OnFinProcesoEventHandler OnFinProceso;

        public delegate void OnCambioEtapaEventHandler(string descripcionEtapa, int cantidadDePasos);
        public event OnCambioEtapaEventHandler OnCambioEtapa;

        public delegate void OnPasoEnEtapaEventHandler(string descripcion);
        public event OnPasoEnEtapaEventHandler OnPasoEnEtapa;

        #endregion


    }
}
