using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Empire_Earth_Launcher
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            Program.LauncherKryptonTheme.AddPalette(launcherKryptonPalette, this);

        }


        // Fake Button as Radio Button

        private void playKryptonCheckButton_Click(object sender, EventArgs e)
        {
            if (!playKryptonCheckButton.Checked)
                playKryptonCheckButton.Checked = true;
            modsKryptonCheckButton.Checked = false;
            settingsKryptonCheckButton.Checked = false;
            launcherKryptonCheckButton.Checked = false;

            generalUserControl.Visible = true;
            settingsUserControl.Visible = false;
            launcherSettingsUserControl.Visible = false;
        }
        private void settingsKryptonCheckButton_Click(object sender, EventArgs e)
        {
            if (!settingsKryptonCheckButton.Checked)
                settingsKryptonCheckButton.Checked = true;
            playKryptonCheckButton.Checked = false;
            modsKryptonCheckButton.Checked = false;
            launcherKryptonCheckButton.Checked = false;

            generalUserControl.Visible = false;
            settingsUserControl.Visible = true;
            launcherSettingsUserControl.Visible = false;
        }

        private void modsKryptonCheckButton_Click(object sender, EventArgs e)
        {
            if (!modsKryptonCheckButton.Checked)
                modsKryptonCheckButton.Checked = true;
            playKryptonCheckButton.Checked = false;
            settingsKryptonCheckButton.Checked = false;
            launcherKryptonCheckButton.Checked = false;


            generalUserControl.Visible = false;
            settingsUserControl.Visible = false;
            launcherSettingsUserControl.Visible = false;
        }

        private void launcherSettingsKryptonCheckButton_Click(object sender, EventArgs e)
        {
            if (!launcherKryptonCheckButton.Checked)
                launcherKryptonCheckButton.Checked = true;
            playKryptonCheckButton.Checked = false;
            modsKryptonCheckButton.Checked = false;
            settingsKryptonCheckButton.Checked = false;

            generalUserControl.Visible = false;
            settingsUserControl.Visible = false;
            launcherSettingsUserControl.Visible = true;
        }
    }
}
