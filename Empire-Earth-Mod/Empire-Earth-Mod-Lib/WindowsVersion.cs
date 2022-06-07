using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace Empire_Earth_Mod_Lib
{
    public static class WindowsVersion
    {
        public enum WindowsVersionEnum
        {
            [Description("Error")] Error = -1,
            [Description("Wine")] Wine,
            [Description("Windows 95")] W95,
            [Description("Windows 98")] W98,
            [Description("Windows Me")] Me,
            [Description("Windows NT")] Nt,
            [Description("Windows 2000")] W2000,
            [Description("Windows XP")] Xp,
            [Description("Windows Vista")] Vista,
            [Description("Windows 7")] Seven,
            [Description("Windows 8")] Eight,
            [Description("Windows 10")] Ten,
            [Description("Windows 11")] Eleven
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] in string lpModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        
        /**
         * Don't forget to add Windows 10 in supportedOS in app.manifest or
         * it will not be able to detect it
         *
         * Microsoft est pas foutu de faire un truc pour identifier la version de Windows...
         * A little dirty I know but It support all versions :V
         */
        public static WindowsVersionEnum GetCurrentWindowsVersion()
        {
            // Based from isWine (https://github.com/zocker-160/isWINE/blob/master/isWINE_dll/isWINE.cpp)
            IntPtr ntPtr = GetModuleHandle("ntdll.dll");
            if (ntPtr != IntPtr.Zero && GetProcAddress(ntPtr, "wine_get_version") != IntPtr.Zero)
                return WindowsVersionEnum.Wine;

            OperatingSystem operatingSystem = Environment.OSVersion;

            switch (operatingSystem.Platform)
            {
                case PlatformID.Win32Windows:
                    //This is a pre-NT version of Windows
                    switch (operatingSystem.Version.Minor)
                    {
                        case 0:
                            return WindowsVersionEnum.W95;
                        case 10:
                            return WindowsVersionEnum.W98;
                        case 90:
                            return WindowsVersionEnum.Me;
                        default:
                            return WindowsVersionEnum.Error;
                    }
                case PlatformID.Win32NT:
                    switch (operatingSystem.Version.Major)
                    {
                        case 3:
                        case 4:
                            return WindowsVersionEnum.Nt;
                        case 5:
                            return operatingSystem.Version.Minor == 0
                                ? WindowsVersionEnum.W2000
                                : WindowsVersionEnum.Xp;
                        case 6:
                            switch (operatingSystem.Version.Minor)
                            {
                                case 0:
                                    return WindowsVersionEnum.Vista;
                                case 1:
                                    return WindowsVersionEnum.Seven;
                                default:
                                {
                                    if (operatingSystem.Version.Minor == 2 || operatingSystem.Version.Minor == 3)
                                        return WindowsVersionEnum.Eight;
                                    else
                                        return WindowsVersionEnum.Error;
                                }
                            }
                        case 10:
                            return operatingSystem.Version.Build < 22000
                                ? WindowsVersionEnum.Ten
                                : WindowsVersionEnum.Eleven;
                        default:
                            return WindowsVersionEnum.Error;
                    }
                default:
                    return WindowsVersionEnum.Error;
            }
        }

        public static string GetWindowsVersionName(WindowsVersionEnum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}