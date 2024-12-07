using SincroStock.Comunes;
using SincroStock.Comunes.Utils;
using SincroStock.Servicio.Negocio;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SincroStock.Servicio
{
    public partial class Servicio : ServiceBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(Servicio));

        public Servicio()
        {
            InitializeComponent();
        }

        #region Service Start/Stop Handlers

        protected override void OnStart(string[] args)
        {
            ControladorServicio.Instance.iniciarTarea();
            LogUtil.Log(logger, Level.Info, "Servicio iniciado");
        }

        protected override void OnStop()
        {
            //Se hace un request de 30 segundos adicionales dado que se estima es lo máximo que podría demorar 
            //la carga de un comprobante por XTango
            this.RequestAdditionalTime(30000);
            ControladorServicio.Instance.detenerTarea();
            LogUtil.Log(logger, Level.Info, "Servicio detenido");
        }

        #endregion Service Start/Stop Handlers
    }
}
