using SincroStock.Comunes;
using SincroStock.Comunes.Utils;
using SincroStock.Servicio.Negocio;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.IO;

namespace SincroStock.Servicio
{
    static class Program
    {
        private static ILog _logger = LogManager.GetLogger(typeof(Program));

        private static bool _ConsolePauseEnd = false;
        public static bool ConsolePauseEnd
        {
            get { return Program._ConsolePauseEnd; }
            set { Program._ConsolePauseEnd = value; }
        }
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        public static int Main(string[] args)
        {
            string sintaxis = "Sintáxis: " + Environment.GetCommandLineArgs()[0] + " [[-install|-i]|[-uninstall|-u]|[-console|-c]]";
            int returnValue = 0;
            string mutexID = "re09e144-8b65-4134-9WWW-677n13m51dd0_SVC";
            bool inicioModoConsola = false;

            try
            {
                ControladorServicio cs = ControladorServicio.Instance;
                ConfigGeneral config = ConfigGeneral.Instance;
                config.cargar();
                config.SetupLogSVC();

                if (args.Length == 0)
                {
                    Servicio svc = null;

                    bool mutexLibre;
                    using (Mutex lockProceso = new Mutex(true, mutexID, out mutexLibre))
                    {
                        if (!mutexLibre)
                            throw new Exception("El servicio ya se encuentra en ejecución");
                        svc = new Servicio();
                        ServiceBase[] ServicesToRun;
                        ServicesToRun = new ServiceBase[] { svc };
                        ServiceBase.Run(ServicesToRun);
                    }
                }
                else if (args.Length > 1)
                {
                    returnValue = -2;
                    throw new Exception("La cantidad de parámetros especificada no es correcta");
                }
                else
                {
                    switch (args[0].ToLower())
                    {
                        case "-install":
                        case "-i":
                            if (cs.EstadoDelServicio == EstadoServicio.NO_INSTALADO)
                            {
                                cs.instalarServicio();
                                Console.WriteLine("Se ha instalado correctamente el servicio");
                            }
                            else
                            {
                                throw new Exception("El servicio ya se encuentra instalado");
                            }
                            break;
                        case "-uninstall":
                        case "-u":
                            if (cs.EstadoDelServicio != EstadoServicio.NO_INSTALADO)
                            {
                                if (cs.EstadoDelServicio == EstadoServicio.INICIADO)
                                    cs.detenerServicio();
                                cs.desinstalarServicio();
                                Console.WriteLine("Se ha desinstalado correctamente el servicio");
                            }
                            else
                            {
                                throw new Exception("El servicio no se encuentra instalado");
                            }
                            break;
                        case "-console":
                        case "-c":
                            inicioModoConsola = true;
                            bool mutexLibre;
                            using (Mutex lockProceso = new Mutex(true, mutexID, out mutexLibre))
                            {
                                if (!mutexLibre)
                                    throw new Exception("El servicio ya se encuentra en ejecución");

                                cs.iniciarTarea();
                                Console.WriteLine("Servicio iniciado en modo consola");
                                Thread.Sleep(Timeout.Infinite);
                            }
                            break;
                        default:
                            returnValue = -2;
                            throw new Exception("Parámetro inválido");
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                try
                {
                    File.AppendAllText(Path.Combine(new GC.Utils.AssemblyInfo(typeof(Program)).Directory, "iniError.log"),
                    Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " | Error al iniciar la aplicación: " + ex.Message
                    + Environment.NewLine + Environment.NewLine + ex.StackTrace);
                }
                catch { }

                if (inicioModoConsola)
                {
                    Console.WriteLine("Error: " + ex.Message + (returnValue == -2 ? Environment.NewLine + Environment.NewLine + sintaxis : ""));
                }
                else
                {
                    try
                    {
                        LogUtil.Log(_logger, Level.Fatal, "Error al iniciar la aplicación.", ex);
                    }
                    catch { }
                    try
                    {
                        string source = new GC.Utils.AssemblyInfo(typeof(Program)).Product;
                        string log = "Application";
                        string sEvent = "Error al iniciar la aplicación:" + ex.Message;
                        if (!EventLog.SourceExists(source))
                            EventLog.CreateEventSource(source, log);

                        EventLog.WriteEntry(source, sEvent, EventLogEntryType.Error);
                    }
                    catch (Exception) { }
                }

                if (returnValue == 0)
                    returnValue = -1;

                return returnValue;
            }
            finally
            {
                if (args.Length != 0 && Program.ConsolePauseEnd)
                {
                    Console.WriteLine(Environment.NewLine + "Presione una tecla para salir...");
                    Console.ReadKey();
                }
            }
        }
    }
}
