using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Empire_Earth_Launcher
{
    public partial class LauncherSettingsUserControl : UserControl
    {
        private DirectoryInfo themeDirectoryInfo;

        public LauncherSettingsUserControl()
        {
            InitializeComponent();
            this.themeDirectoryInfo = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "themes"));

            Program.LauncherKryptonTheme.AddPalette(launcherKryptonPalette, this);

            GetThemeAvailaible();
        }

        private void GetThemeAvailaible()
        {
            if (!themeDirectoryInfo.Exists)
                return;

            foreach (FileInfo fileinfo in themeDirectoryInfo.GetFiles())
            {
                themeKryptonComboBox.Items.Add(Path.GetFileNameWithoutExtension(fileinfo.Name));
            }
        }

        private void themeKryptonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_theme_name = (string) themeKryptonComboBox.SelectedItem;

            if (themeKryptonComboBox.SelectedIndex > 0)
                Program.LauncherKryptonTheme.SwitchThemeFromName(selected_theme_name);
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Theme File (*.xml)|*.xml|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog.FileName == null)
                        return;
                    Program.LauncherKryptonTheme.SwitchThemeFromFile(openFileDialog.FileName);
                }
            }

        }
    }
}
