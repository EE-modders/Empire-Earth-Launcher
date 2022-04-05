using Krypton.Toolkit;
using Empire_Earth_Launcher.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Empire_Earth_Launcher
{
    public class LauncherKryptonTheme
    {
        private Dictionary<KryptonPalette, Control> kryptonPaletteList;
        private string currentThemeFile;
        private DirectoryInfo launcherThemesDirInfo;

        public LauncherKryptonTheme(string defaultThemeFile = null)
        {
            launcherThemesDirInfo = new DirectoryInfo(Environment.CurrentDirectory + @"\themes");
            kryptonPaletteList = new Dictionary<KryptonPalette, Control>();

            if (defaultThemeFile != null)
                currentThemeFile = defaultThemeFile;
            else
                currentThemeFile = Path.Combine(launcherThemesDirInfo.FullName, "Light.xml");
        }

        public void AddPalette(KryptonPalette kryptonPalette, Control control)
        {
            kryptonPaletteList.Add(kryptonPalette, control);
            SwitchIndividualThemeName(kryptonPalette, control, currentThemeFile);
        }

        private void SwitchIndividualThemeName(KryptonPalette kryptonPalette, Control control, string themeFile)
        {
            try
            {
                kryptonPalette.Import(themeFile);
            }
            catch (ArgumentException)
            {
                Program.Logging.Log("Unnable load the theme file " + Path.GetFileName(themeFile) + " !", Logging.LogLevel.Warning);
                return;
            }

            if (control != null && kryptonPaletteList.Count != 0)
            {
                control.BackColor = kryptonPalette.FormStyles.FormCommon.StateCommon.Back.Color1;
                if (control is KryptonForm || control is Form)
                {
                    ((Form)control).Icon = Resources.EmpireEarthLauncher;
                }
            }
        }

        public void SwitchThemeFromName(string themeName)
        {
            FileInfo themeFile = new FileInfo(Path.Combine(launcherThemesDirInfo.FullName, themeName + ".xml"));
            if (!themeFile.Exists)
            {
                Program.Logging.Log("Unnable find the theme file " + themeFile.Name + " !", Logging.LogLevel.Warning);
                return;
            }

            foreach (KryptonPalette kryptonPalette in kryptonPaletteList.Keys)
            {
                SwitchIndividualThemeName(kryptonPalette, kryptonPaletteList[kryptonPalette], themeFile.FullName);
            }
        }

        public void SwitchThemeFromFile(string themeFilePath)
        {
            FileInfo themeFile = new FileInfo(themeFilePath);
            if (!themeFile.Exists)
            {
                Program.Logging.Log("Unnable find the theme file " + themeFile.Name + " !", Logging.LogLevel.Warning);
                return;
            }

            foreach (KryptonPalette kryptonPalette in kryptonPaletteList.Keys)
            {
                SwitchIndividualThemeName(kryptonPalette, kryptonPaletteList[kryptonPalette], themeFile.FullName);
            }
        }

    }
}
