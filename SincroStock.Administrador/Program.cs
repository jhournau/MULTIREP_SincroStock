using SincroStock.Comunes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincroStock.Administrador
{
    static class Program
    {
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;

        private static Form iniForm;


        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //Garantiza instancia única de la aplicación
            bool mutexLibre;

            using (Mutex lockProceso = new Mutex(true, "Instancia unica Administador " + ConfigGeneral.Instance.GUID, out mutexLibre))
            {
                if (!mutexLibre)
                {
                    mostrarVentanaApp();
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                iniForm = new FormAdministrador();
                Application.Run(iniForm);
            }
        }


        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private static void mostrarVentanaApp()
        {
            try
            {
                Process p = Process.GetProcessesByName("IFC_TangoVtex_Administrador").ElementAt(0);
                ShowWindowAsync(p.MainWindowHandle, 1);
                SetForegroundWindow(p.MainWindowHandle);
            }
            catch { }
        }

        public static void Exit()
        {
            iniForm.Close();
        }
    }
}
