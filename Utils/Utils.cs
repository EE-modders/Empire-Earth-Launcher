using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;

namespace EELauncher
{
    class Utils
    {

        public enum WindowsVersion
        {
            OLD = 0, Win2000 = 1, WinXP = 2, WinVista = 3, Win7 = 4, Win8orMore = 5
        }

        public static bool dgVoodooSupport()
        {
            if (GetCurrentWindowsVersion() >= WindowsVersion.Win7) // Maybe Vista too idk
            {
                return true;
            }

            return false;
        }

        public static bool discordSupport()
        {
            if (GetCurrentWindowsVersion() >= WindowsVersion.Win7)
            {
                return true;
            }

            return false;
        }

        public static bool isWindowsOld()
        {
            if (GetCurrentWindowsVersion() <= WindowsVersion.WinVista)
            {
                return true;
            }

            return false;
        }

        public static WindowsVersion GetCurrentWindowsVersion()
        {
            int major = Environment.OSVersion.Version.Major;
            int minor = Environment.OSVersion.Version.Minor;

            if (major >= 6 && minor >= 2)
            {
                return WindowsVersion.Win8orMore;
            }
            else if (major == 6 && minor >= 1)
            {
                return WindowsVersion.Win7;
            }
            else if (major == 6 && minor <= 1)
            {
                return WindowsVersion.WinVista;
            }
            else if (major == 5 && minor >= 1)
            {
                return WindowsVersion.WinXP;
            }
            else if (major == 5)
            {
                return WindowsVersion.Win2000;
            }
            else
            {
                return WindowsVersion.OLD;
            }
        }

        public static bool IsCurrentExecAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent())
                .IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void RestartAsAdmin()
        {
            string fileName = Assembly.GetExecutingAssembly().Location;
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.Verb = "runas";
            processInfo.FileName = fileName;

            try
            {
                Process.Start(processInfo);
            }
            catch
            {
                MessageBox.Show("Unnable to start the game, please uninstall it and install it as user...");
            }

            Environment.Exit(0);
        }

    }
}
