using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Empire_Earth_Launcher
{
    public partial class SettingsUserControl : UserControl
    {
        public SettingsUserControl()
        {
            InitializeComponent();
            Program.LauncherKryptonTheme.AddPalette(launcherKryptonPalette, this);
        }

        private void compatibilityWarningConfirmationKryptonButton_Click(object sender, EventArgs e)
        {
            compatibilityWarningKryptonPanel.Visible = false;
            new LauncherDialog("Simple Question Dialog", "This a very basic question blabla\nanother line here wow", MessageBoxButtons.OK).ShowDialog();
        }
    }
}
