using System;
using System.Windows.Forms;

namespace EELauncher
{
    public partial class LauncherForm : Form
    {
        public static bool storage_online = false;

        public LauncherForm()
        {
            InitializeComponent();

            // Will be enabled after connection check
            playButton.Enabled = false;
            storage_online = false;

            RefreshHelpLabels();

            discordCheckBox.Enabled = Utils.discordSupport();
            oldHardwareCheckBox.Checked = Utils.isWindowsOld();
            dgVoodooComboBox.Enabled = Utils.dgVoodooSupport();

            RemoteData();
        }

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void RefreshHelpLabels()
        {
            bool state = helperCheckBox.Checked;
            menuHelpLabel.Visible = state;
            dgVoodooHelpLabel.Visible = state;
            eehelperHelperLabel.Visible = state;
            setupHelperLabel.Visible = state;
            portableHelperLabel.Visible = state;
            shortcutHelpLabel.Visible = state;
            debugButton.Visible = state;
        }

        private void RemoteData()
        {
            // All 'Remote Data' need to to manage no internet
            new PingHelper("storage.empireearth.eu", 80).TestConnection(energyServerConLabel, playButton);
            new PingHelper("titan.neoee.net", 80).TestConnection(neoServerConLabel, playButton);
            new LanguagesHelper("http://storage.empireearth.eu/Launcher/languages.txt").PopList(langComboBox);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void helperCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshHelpLabels();
        }

        private void shortcutsButton_Click(object sender, EventArgs e)
        {
            new ShortcutsForm().ShowDialog();
        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Detected Windows Version = " + Utils.GetCurrentWindowsVersion() + "\n"
                + "dgVoodoo Support = " + Utils.dgVoodooSupport() + "\n"
                + "Old Hardware (DLL) = " + Utils.isWindowsOld() + " (overwriteable)\n"
                + "Discord Support = " + Utils.discordSupport() + "\n"
                + "NeoEE EE = " + new GameHelper(GameHelper.GameType.EE, GameHelper.GameProvider.Neo).IsInstalled() + "\n"
                );
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (langComboBox.SelectedIndex == -1 ||
                menuComboBox.SelectedIndex == -1 ||
                dgVoodooComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all ComboBox to start the game...", "Dude...");
                return;
            }

            GameHelper gameHelper = new GameHelper(GameHelper.GameType.EE, GameHelper.GameProvider.Neo);

            if (gameHelper.IsInstalled())
            {
                if (storage_online)
                {
                    progressBar.Visible = true;
                    progressBar.Style = ProgressBarStyle.Marquee;
                    progressBar.Value = 100;

                    if (!Utils.IsCurrentExecAdmin() && gameHelper.NeedAdmin())
                    {
                        MessageBox.Show("Game is installed as Admin but Launcher is running as user...\n\n" +
                            "Restarting Launcher as Admin...", "Not Admin !", MessageBoxButtons.OK);
                        Utils.RestartAsAdmin();
                    }

                    new ContentUpdater(gameHelper).UpdateLang(langComboBox.Text);

                    progressBar.Visible = false;
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressBar.Value = 0;
                }
                else
                {
                    MessageBox.Show("Game is starting, but storage server are offline !\n" +
                        "The game can't be updated or verified...", "Storage Server Offline !");
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(
                    gameHelper.GetName() + " is not installed !\n" +
                    "Do you want EE Launcher to install it ?",
                    "Install " + gameHelper.GetName() + " ? ",
                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    if (storage_online)
                    {
                        gameHelper.Install(this, progressBar, downloadLabel);
                    }
                    else
                    {
                        MessageBox.Show("Unnable to install !\nStorage Server is Offline !");
                    }
                }
            }
        }

        private void individualSetupbutton_Click(object sender, EventArgs e)
        {
            new IndividualSetupForm().ShowDialog();
        }

        private void customizeLobbyButton_Click(object sender, EventArgs e)
        {
            new CustomizeLobbyForm(new GameHelper(GameHelper.GameType.EE, GameHelper.GameProvider.Neo)).ShowDialog();
        }
    }
}
