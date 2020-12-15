using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;

namespace EELauncher
{
    class PingHelper
    {

        private string ip;
        private int port;
        private Label info_label;
        private Button playButton;

        public PingHelper(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public void TestConnection(Label info_label, Button playButton)
        {
            this.info_label = info_label;
            this.playButton = playButton;
            BackgroundWorker connectionBackgroundWorker = new BackgroundWorker();
            connectionBackgroundWorker.DoWork += ConnectionBackgroundWorker_DoWork;
            connectionBackgroundWorker.RunWorkerCompleted += ConnectionBackgroundWorker_RunWorkerCompleted;
            connectionBackgroundWorker.RunWorkerAsync();
        }

        private void ConnectionBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            playButton.Enabled = true;

            if (ip.Equals("storage.empireearth.eu"))
            {
                LauncherForm.storage_online = (bool)e.Result;
            }

            if ((bool)e.Result)
            {
                info_label.ForeColor = Color.Green;
                info_label.Text = "ONLINE";
            }
            else
            {
                info_label.ForeColor = Color.Red;
                info_label.Text = "OFFLINE";
            }
        }

        private void ConnectionBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    if (!client.BeginConnect(ip, port, null, null).AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5)))
                    {
                        e.Result = false;
                        return;
                    }
                    client.Close();
                }
            }
            catch (Exception)
            {
                e.Result = false;
                return;
            }

            e.Result = true;
        }
    }
}
