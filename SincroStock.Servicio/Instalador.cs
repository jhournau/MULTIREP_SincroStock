using SincroStock.Comunes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace SincroStock.Servicio
{
    [RunInstaller(true)]
    public partial class Instalador : Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;
        public Instalador()
        {
            ConfigGeneral cfg = ConfigGeneral.Instance;

            InitializeComponent();
            // Instantiate installers for process and services.
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.DelayedAutoStart = true;
            serviceInstaller.ServiceName = cfg.NombreServicio;
            serviceInstaller.DisplayName = cfg.NombreApp;
            serviceInstaller.Description = "Ejecuta el proceso de carga de " + cfg.NombreApp + " según la planificación establecida.";
            processInstaller.Account = ServiceAccount.LocalSystem;
            processInstaller.Username = null;
            processInstaller.Password = null;
            processInstaller.
            // Add installers to collection. Order is not important.
            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }
    }
}
