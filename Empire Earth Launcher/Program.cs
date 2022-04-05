using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Empire_Earth_Launcher
{
    static class Program
    {
        
        public readonly static Logging Logging =  new Logging("log.txt");
        public readonly static LauncherKryptonTheme LauncherKryptonTheme = new LauncherKryptonTheme();

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Logging.Log("Starting Empire Earth Launcher v" + Application.ProductVersion);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            LauncherKryptonTheme.SwitchThemeFromName("Light");

            Logging.Log("Starting Empire Earth Launcher Form");
            Application.Run(new Form1());
        }
    }
}
