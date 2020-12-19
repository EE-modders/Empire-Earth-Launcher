using System;
using System.ComponentModel;
using System.Threading;
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

            if (drexCheckBox.Checked == false)
            {
                menuComboBox.Enabled = false;
            }

            new LanguagesHelper(null).OfflinePop(langComboBox);
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
                + "NeoEE EE = " + new GameHelper(GameHelper.GameType.EE, GameHelper.GameProvider.Neo).IsInstalled()
                );
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (langComboBox.SelectedIndex == -1 ||
                dgVoodooComboBox.SelectedIndex == -1 || (drexCheckBox.Checked &&
                menuComboBox.SelectedIndex == -1))
            {
                MessageBox.Show("Please fill all ComboBox to start the game...", "Dude...");
                return;
            }

            GameHelper gameHelper = new GameHelper(GameHelper.GameType.EE, GameHelper.GameProvider.Neo);

            if (gameHelper.IsInstalled())
            {
                if (storage_online)
                {
                    if (!Utils.IsCurrentExecAdmin() && gameHelper.NeedAdmin())
                    {
                        MessageBox.Show("Game is installed as Admin but Launcher is running as User...\n" +
                            "Restarting Launcher as Admin...", "Not Admin !", MessageBoxButtons.OK);
                        Utils.RestartAsAdmin();
                    }

                    string lang = langComboBox.Text;
                    bool skipIntro = skipIntrocheckBox.Checked;
                    bool fullGame = fullGameCheckBox.Checked;
                    bool dreXmod = drexCheckBox.Checked;

                    SynchronizationContext uiSynch = SynchronizationContext.Current;

                    BackgroundWorker backgroundWorker = new BackgroundWorker();
                    backgroundWorker.DoWork += delegate (object send, DoWorkEventArgs eve)
                    {
                        ContentUpdater contentUpdater = new ContentUpdater(gameHelper, downloadLabel, progressBar, uiSynch);
                        contentUpdater.UpdateLang(lang);

                        if (fullGame)
                        {
                            contentUpdater.UpdateIntro(skipIntro, lang);
                            contentUpdater.UpdateData(lang);
                        }
                    };

                    backgroundWorker.RunWorkerCompleted += delegate (object send, RunWorkerCompletedEventArgs eve)
                    {
                        MessageBox.Show("AFTER UPDATE");
                        gameHelper.Start();
                    };

                    backgroundWorker.RunWorkerAsync();

                }
                else
                {
                    string lang = langComboBox.Text;
                    bool skipIntro = skipIntrocheckBox.Checked;
                    SynchronizationContext uiSynch = SynchronizationContext.Current;

                    BackgroundWorker backgroundWorker = new BackgroundWorker();
                    backgroundWorker.DoWork += delegate (object send, DoWorkEventArgs eve)
                    {
                        ContentUpdater contentUpdater = new ContentUpdater(gameHelper, downloadLabel, progressBar, uiSynch);
                        contentUpdater.UpdateLangOffline(lang);
                    };

                    backgroundWorker.RunWorkerCompleted += delegate (object send, RunWorkerCompletedEventArgs eve)
                    {
                        MessageBox.Show("AFTER UPDATE OFFLINE");
                    };

                    MessageBox.Show("Game is starting, but storage server are offline !\n" +
                        "The game can't be updated or verified...\nOnly some EE Launcher feature will work...", "Storage Server Offline !");
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

        private void drexCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            menuComboBox.Enabled = drexCheckBox.Checked;
        }

        private void ShowHelp(string message)
        {
            MessageBox.Show(message, "Help", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void langHelpLabel_Click(object sender, EventArgs e)
        {
            ShowHelp("Allows you to change the display language of the game, and the rest of the content if you also use the 'Full Game' option." +
                "\n\nSome languages may not work on your computer because their format requires Windows to be installed with the language selected." +
                "\nYou should still be able to display the language you want by changing the Region settings in the control panel under " +
                "'Language for non-unicode programs', also on the latest version of Windows 10 an option allows you to support all formats (UTF-8) " +
                "allowing you to play with all languages.");
        }

        private void menuHelpLabel_Click(object sender, EventArgs e)
        {
            ShowHelp("This is the menu resolution (so not the game resolution), to use it you have to enable dreXmod.");
        }

        private void dgVoodooHelpLabel_Click(object sender, EventArgs e)
        {
            ShowHelp("dgVoodoo is a library that converts old display instructions into modern display instructions.This solves a lot of problems in " +
                "the vast majority of cases." +
                "\n\nNVIDIA: Usually not necessary, but can fix problems." +
                "\nAMD: Can fix problems and improve performance" +
                "\nIntel: Can triple performance and fix problems" +
                "\n\nHowever, if your game is already running very well this option is not recommended because it can sometimes create annoying " +
                "slowdowns (especially when you use the in game interface...We do not know why...)");
        }
    }
}
