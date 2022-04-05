using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empire_Earth_Launcher
{
    public partial class GeneralUserControl : UserControl
    {
        private BackgroundWorker backgroundWorker;
        private WON.LobbyPersistentData.LobbyUserData lobbyUserData;

        public GeneralUserControl()
        {
            InitializeComponent();
            Program.LauncherKryptonTheme.AddPalette(launcherKryptonPalette, this);

            backgroundWorker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerAsync();


            WON.LobbyPersistentData.LobbyGlobalData test = new WON.LobbyPersistentData.LobbyGlobalData("./_wonlobbypersistent.dat");
            foreach (var lobbyGlobalData in test.PlayerInfoGlobalDatas.OrderByDescending(d => d.LastUse))
            {
                usersLobbyKryptonComboBox.Items.Add(lobbyGlobalData.Username);
            }
            usersLobbyKryptonComboBox.SelectedIndex = 0;
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            WON.NeoAPI.ConnectedPlayersMessage ConnectedPlayersMessage = ((WON.NeoAPI.ConnectedPlayersMessage)e.UserState);
            onlinePlayersKryptonDataGridView.Rows.Clear();

            neoOnlineKryptonGroupBox.Values.Heading = "Online Players (" + ConnectedPlayersMessage.OnlinePlayers + ")";

            foreach (WON.NeoAPI.ConnectedPlayersMessage.PlayerInfo pInfo in ConnectedPlayersMessage.PlayersInfo)
            {
                if (!usersLobbyKryptonComboBox.Text.Equals(pInfo.Name, StringComparison.InvariantCultureIgnoreCase))
                    onlinePlayersKryptonDataGridView.Rows.Add(pInfo.Name + pInfo.Name, pInfo.GameStateToString(pInfo.GameState));
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            while (!worker.CancellationPending)
            {
                worker.ReportProgress(0, new WON.NeoAPI.ConnectedPlayersMessage());
                Thread.Sleep(5000);
            }
        }

        private void usersLobbyKryptonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WON.LobbyPersistentData.LobbyGlobalData test = new WON.LobbyPersistentData.LobbyGlobalData("./_wonlobbypersistent.dat");

            int lobbyFileId = -1;
            foreach (WON.LobbyPersistentData.LobbyGlobalData.PlayerInfoGlobalData lobbyGlobalData in test.PlayerInfoGlobalDatas)
            {
                if (lobbyGlobalData.Username.Equals(usersLobbyKryptonComboBox.Text))
                {
                    lobbyFileId = lobbyGlobalData.FileID;
                    break;
                }
            }

            if (lobbyFileId == -1)
                return;

            string tmppath = @"C:\Program Files (x86)\Neo Empire Earth\Empire Earth";
            FileInfo fileInfo = new FileInfo(Path.Combine(tmppath, "_wonuser" + lobbyFileId + ".dat"));

            if (!fileInfo.Exists)
                return;
            lobbyUserData = new WON.LobbyPersistentData.LobbyUserData(fileInfo.FullName);
            neoOnlineKryptonGroupBox.Values.Description = "Friends (" + lobbyUserData.Friends.Count + ")";
        }
    }
}
