using GC.Utils.Helpers;
using SincroStock.Comunes;
using SincroStock.Comunes.Negocio;
using log4net;
using log4net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SincroStock.Comunes.Utils;
using SincroStock.Comunes.Exceptions;

namespace SincroStock.Servicio.Negocio
{
    public enum EstadoServicio
    {
        [EnumDescription("No instalado")]
        NO_INSTALADO = 0,
        [EnumDescription("Iniciando")]
        INICIANDO = ServiceControllerStatus.StartPending,
        [EnumDescription("Iniciado")]
        INICIADO = ServiceControllerStatus.Running,
        [EnumDescription("Detenido")]
        DETENIDO = ServiceControllerStatus.Stopped,
        [EnumDescription("Deteniendo")]
        DETENIENDO = ServiceControllerStatus.StopPending,
        [EnumDescription("Pausado")]
        PAUSADO = ServiceControllerStatus.Paused,
        [EnumDescription("Pausando")]
        PAUSANDO = ServiceControllerStatus.PausePending,
        [EnumDescription("Reanundando")]
        REANUDANDO = ServiceControllerStatus.ContinuePending
    }

    public class ControladorServicio
    {
        #region Constantes, Atributos, y Propiedades

        private static ILog logger = LogManager.GetLogger(typeof(ControladorServicio));

        private static ControladorServicio instance = null;

        private ConfigGeneral config;
        private System.Threading.Timer timer;
        private DateTime horaUltimoInformeErrorPlanificacion = DateTime.MinValue;
        private DateTime? momentoUltimaEjecucion = null;

        private const int TOPE_EN_MINUTOS_ERRORES_PLANIFICADOR = 15;

        private DateTime ultimaCargaConfig = DateTime.MinValue;

        private string nombreServicio;
        //private bool notificarTimeoutTangoAlcanzado = false;

        public EstadoServicio EstadoDelServicio
        {
            get
            {
                try
                {
                    ServiceController sc = new ServiceController();
                    sc.ServiceName = nombreServicio;
                    EstadoServicio svcEstado = (EstadoServicio)sc.Status;
                    return svcEstado;
                }
                catch (InvalidOperationException)
                {
                    return EstadoServicio.NO_INSTALADO;
                }
            }
        }

        #endregion Constantes, Atributos, y Propiedades

        #region Constructor

        private ControladorServicio()
        {
            config = ConfigGeneral.Instance;
            nombreServicio = ConfigGeneral.Instance.NombreServicio;
            //configuracionActualizada = false;
        }

        #endregion Constructor

        public static ControladorServicio Instance
        {
            get
            {
                if (instance == null)
                    instance = new ControladorServicio();
                return instance;
            }
        }

        #region Planificador

        private bool debeEjecutarTarea(DateTime momentoActual, DateTime? momentoUltimaEjecucion)
        {

            if (!config.PlanificacionEjecucionActivada)
                return false;

            if (!((momentoActual.DayOfWeek == DayOfWeek.Monday && this.config.PlanificacionEjecutaLunes) ||
                 (momentoActual.DayOfWeek == DayOfWeek.Tuesday && this.config.PlanificacionEjecutaMartes) ||
                 (momentoActual.DayOfWeek == DayOfWeek.Wednesday && this.config.PlanificacionEjecutaMiercoles) ||
                 (momentoActual.DayOfWeek == DayOfWeek.Thursday && this.config.PlanificacionEjecutaJueves) ||
                 (momentoActual.DayOfWeek == DayOfWeek.Friday && this.config.PlanificacionEjecutaViernes) ||
                 (momentoActual.DayOfWeek == DayOfWeek.Saturday && this.config.PlanificacionEjecutaSabado) ||
                 (momentoActual.DayOfWeek == DayOfWeek.Sunday && this.config.PlanificacionEjecutaDomingo)))
                return false;

            if (config.PlanificacionEjecutaAUnaHoraEspecifica)
            {
                DateTime momentoActualNormalizado = new DateTime(momentoActual.Year, momentoActual.Month, momentoActual.Day, momentoActual.Hour, momentoActual.Minute, 0, 0);
                DateTime? momentoUltimaEjecucionNormalizado = momentoUltimaEjecucion == null ? null : (DateTime?)new DateTime(momentoUltimaEjecucion.Value.Year, momentoUltimaEjecucion.Value.Month, momentoUltimaEjecucion.Value.Day, momentoUltimaEjecucion.Value.Hour, momentoUltimaEjecucion.Value.Minute, 0, 0);

                if (momentoActualNormalizado.Hour == config.PlanificacionHoraEjecucion.Hour && momentoActualNormalizado.Minute == config.PlanificacionHoraEjecucion.Minute &&
                    (momentoUltimaEjecucionNormalizado == null || momentoUltimaEjecucionNormalizado.Value.CompareTo(momentoActualNormalizado) != 0))
                    return true;
                else
                    return false;
            }
            else if (config.PlanificacionEjecutaCadaXUnidades)
            {
                DateTime horaDesde = new DateTime(momentoActual.Year, momentoActual.Month, momentoActual.Day, config.PlanificacionHoraEjecucionDesde.Hour, config.PlanificacionHoraEjecucionDesde.Minute, 0, 0);
                DateTime horaHasta = new DateTime(momentoActual.Year, momentoActual.Month, momentoActual.Day, config.PlanificacionHoraEjecucionHasta.Hour, config.PlanificacionHoraEjecucionHasta.Minute, 0, 0).AddMinutes(1).AddMilliseconds(-1);

                bool cumplePeriodoDeEjecucion = !momentoUltimaEjecucion.HasValue || (diferenciaEnMinutos(momentoActual, momentoUltimaEjecucion.Value) >= getMinutos(config.PlanificacionUnidadTiempo, config.PlanificacionPeriodo));
                bool cumpleRangoHorario = DateTime.Compare(horaDesde, horaHasta) < 0 ?
                    DateTime.Compare(momentoActual, horaDesde) >= 0 && DateTime.Compare(momentoActual, horaHasta) <= 0 :
                    DateTime.Compare(momentoActual, horaDesde) >= 0 || DateTime.Compare(momentoActual, horaHasta) <= 0;

                return cumplePeriodoDeEjecucion && cumpleRangoHorario;
            }
            else
                return false;
        }

        private long getMinutos(UnidadTiempo unidad, int valor)
        {
            if (unidad == UnidadTiempo.HORA)
                return valor * 60;
            else if (unidad == UnidadTiempo.MINUTO)
                return valor;

            return -1;
        }

        private long diferenciaEnMinutos(DateTime fecha1, DateTime fecha2)
        {
            if (fecha1.CompareTo(fecha2) < 0)
                return -1;
            else
            {
                TimeSpan ts = fecha1 - fecha2;
                return Convert.ToInt64(ts.TotalMinutes);
            }
        }

        private void timerCallbackHander(object stateInfo)
        {
            try
            {
                DateTime instanteActual = DateTime.Now;

                bool mutexLibre;

                using (Mutex lockProceso = new Mutex(true, config.NombreMutexSVC, out mutexLibre))
                {
                    if (!mutexLibre)
                        return;
                    else
                    {
                        if ((DateTime.Now - ultimaCargaConfig).TotalSeconds > 5)
                        {
                            ultimaCargaConfig = DateTime.Now;
                            config.cargar();
                            config.SetupLogSVC();
                        }
                        if (this.debeEjecutarTarea(instanteActual, momentoUltimaEjecucion))
                        {
                            momentoUltimaEjecucion = instanteActual;
                            this.ejecutarTarea();
                        }
                    }
                }
                //System.GC.Collect();
            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;
                if (Convert.ToInt64((now - horaUltimoInformeErrorPlanificacion).TotalMinutes) >= TOPE_EN_MINUTOS_ERRORES_PLANIFICADOR)
                {
                    LogUtil.Log(logger, Level.Fatal, "Error grave al analizar la planificación de la tarea. Revise el log de aplicación.", true, false, ex);
                    horaUltimoInformeErrorPlanificacion = now;
                }
            }
        }

        #endregion

        #region Control de la tarea

        public void iniciarTarea()
        {
            try
            {
                LogUtil.Log(logger, Level.Debug, "Iniciando tarea...");
                this.momentoUltimaEjecucion = null;
                timer = new System.Threading.Timer(new TimerCallback(this.timerCallbackHander), null, 2000, 1000);
            }
            catch (Exception ex)
            {
                LogUtil.Log(logger, Level.Fatal, "Error al iniciar tarea.", ex);
            }
        }

        public void detenerTarea()
        {
            try
            {
                LogUtil.Log(logger, Level.Debug, "Deteniendo tarea...");
                ManualResetEvent disposed = new ManualResetEvent(false);

                if (this.timer != null)
                {
                    ControladorInterfaz.Instance.InterrumpirProceso = true;
                    this.timer.Dispose(disposed);
                    disposed.WaitOne();
                }
            }
            catch (Exception ex)
            {
                LogUtil.Log(logger, Level.Fatal, "Error al detener tarea.", ex);
            }
            finally
            {
                //LogUtil.FlushMail();
            }
        }

        private void ejecutarTarea()
        {
            try
            {
                ControladorInterfaz.Instance.Modo = ModoEjecucion.SERVICIO;
                ControladorInterfaz.Instance.iniciarTarea();
            }
            catch(UserAbortException)
            {
                LogUtil.Log(logger, Level.Info, "Proceso cancelado");
            }
            catch (Exception ex)
            {
                LogUtil.Log(logger, Level.Error, "Error al ejecutar la tarea", true, false, ex);
            }
            finally
            {
                //this.notificarTimeoutTangoAlcanzado = false;
                LogUtil.Log(logger, Level.Debug, "Ejecución de tarea planificada finalizada.");
            }
        }


        #endregion Control de la tarea

        #region Control del servicio

        private void ejecutarProcesoDeInstalacion(bool desinstalar)
        {
            using (AssemblyInstaller inst = new AssemblyInstaller(typeof(Program).Assembly, new string[] { }))
            {
                IDictionary state = new Hashtable();
                inst.UseNewContext = true;
                try
                {
                    if (desinstalar)
                    {
                        LogUtil.Log(logger, Level.Debug, "Se desinstalará el servicio");
                        inst.Uninstall(state);
                    }
                    else
                    {
                        LogUtil.Log(logger, Level.Debug, "Se instalará el servicio");
                        inst.Install(state);
                        inst.Commit(state);
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        inst.Rollback(state);
                    }
                    catch { }
                    throw ex;
                }
            }
        }

        public void instalarServicio()
        {
            try
            {
                ejecutarProcesoDeInstalacion(false);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo instalar el servicio: " + ex.Message, ex);
            }
        }

        public void desinstalarServicio()
        {
            try
            {
                ejecutarProcesoDeInstalacion(true);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo desinstalar el servicio: " + ex.Message, ex);
            }
        }

        public void iniciarServicio()
        {
            ServiceController sc = new ServiceController();
            sc.ServiceName = config.NombreServicio;
            sc.MachineName = Dns.GetHostName();
            sc.Start();
            sc.WaitForStatus(ServiceControllerStatus.Running);
            if (sc.Status != ServiceControllerStatus.Running)
                throw new ApplicationException("No se pudo iniciar el servicio");
        }

        public void detenerServicio()
        {
            ServiceController sc = new ServiceController();
            sc.ServiceName = config.NombreServicio;
            sc.MachineName = Dns.GetHostName();
            sc.Stop();
            sc.WaitForStatus(ServiceControllerStatus.Stopped);
            if (sc.Status != ServiceControllerStatus.Stopped)
                throw new ApplicationException("No se pudo detener el servicio");
        }



        #endregion Control del servicio
    }

}