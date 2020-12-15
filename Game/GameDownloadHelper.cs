using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace EELauncher
{
    class GameDownloadHelper
    {
        private Stopwatch sw = new Stopwatch();

        private GameHelper gameHelper;
        private LauncherForm launcherForm;
        private string url, location;
        private ProgressBar progressBar;
        private Label label;

        private bool install = false;

        public GameDownloadHelper(GameHelper gameHelper, LauncherForm launcherForm, ProgressBar progressBar, Label label, string location, bool install)
        {
            this.gameHelper = gameHelper;
            this.location = location;
            this.progressBar = progressBar;
            this.label = label;
            this.launcherForm = launcherForm;
            this.install = install;
            url = gameHelper.GetSetupURL();
        }

        public void DownloadFile()
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Credentials = CredentialCache.DefaultCredentials;
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                Uri URL = new Uri(url);

                sw.Start();

                try
                {
                    if (File.Exists(location))
                    {
                        File.Delete(location);
                    }

                    webClient.DownloadFileAsync(URL, location);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unnable to download : " + Path.GetFileName(url)
                        + "Error : " + ex.Message, "Download Error !");
                }
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            label.Text = string.Format("{0} MB/s  |  {1} %  ({2}/{3} MB)",
                (e.BytesReceived / 1024d / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"),
                e.ProgressPercentage.ToString(),
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));

            progressBar.Value = e.ProgressPercentage;

        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            sw.Reset();

            label.Text = "";
            label.Visible = false;
            progressBar.Value = 0;
            progressBar.Visible = false;

            if (install)
            {
                DialogResult dialogResult;
                if (Utils.isWindowsOld())
                {
                    dialogResult = MessageBox.Show(
                        "OLD WINDOWS VERSION !!\n" +
                        "You will have to choice yourself the install mode !\n",
                        "Old Windows Version Warning", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.Cancel)
                    {
                        return;
                    }

                    Process.Start(location);
                    label.Text = "";
                    label.Visible = false;
                    launcherForm.Enabled = true;
                    label.Visible = false;
                    return;
                }
                else
                {
                    dialogResult = MessageBox.Show(
                        "Do you want to install the Game as Admin or Current User ?\n" +
                        "Yes : Admin (Need Permission)\n" +
                        "No : Current User\n",
                        "Install Game as ?", MessageBoxButtons.YesNoCancel);
                    if (dialogResult == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                string args = "";
                try
                {
                    args = new WebClient().DownloadString("http://storage.empireearth.eu/Setup/EnergySetupNeoEE.args");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unnable to download install args...\n" +
                        "Minimal installation will be installed...\n" +
                        "Error : " + ex.Message);
                }

                Process processInstall = new Process();
                ProcessStartInfo processStartInfoInstall = new ProcessStartInfo();
                processStartInfoInstall.FileName = new FileInfo(location).FullName;
                processStartInfoInstall.Arguments = "/NORESTART /VERYSILENT /PASSWORD=energy";

                if (dialogResult == DialogResult.Yes)
                {
                    processStartInfoInstall.Arguments += " /ALLUSERS";
                }

                if (dialogResult == DialogResult.No)
                {
                    processStartInfoInstall.Arguments += " /CURRENTUSER";
                }

                DialogResult dialogResultAddon = MessageBox.Show(
                    "Do you want to download Campaigns & Voice for the selected language ?\n" +
                    "You can download this extra content at any time from EE Launcher !", "Install Campaigns & Voice ?",
                    MessageBoxButtons.YesNo);

                if (dialogResultAddon == DialogResult.Yes)
                {
                    processStartInfoInstall.Arguments += " /COMPONENTS=\"" + args + "\"";
                    label.Text = "Installing " + gameHelper.GetName() + " (Full Game)...";
                }
                else
                {
                    processStartInfoInstall.Arguments += " /COMPONENTS=\"!*\"";
                    label.Text = "Installing " + gameHelper.GetName() + "...";
                }

                processInstall.StartInfo = processStartInfoInstall;

                label.Visible = true;
                progressBar.Value = 100;
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;
                BackgroundWorker installBackgroundWorker = new BackgroundWorker();
                installBackgroundWorker.DoWork += InstallWork;
                installBackgroundWorker.RunWorkerCompleted += InstallWorkComplete;
                installBackgroundWorker.RunWorkerAsync(processInstall);
            }
            else
            {
                MessageBox.Show("Download Complete !\nFile Location : " + location, "Download Complete !");
            }
        }

        private void InstallWorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            label.Text = "";
            label.Visible = false;
            launcherForm.Enabled = true;
            label.Visible = false;
            progressBar.Value = 0;
            progressBar.Style = ProgressBarStyle.Blocks;
            progressBar.Visible = false;
        }

        private void InstallWork(object sender, DoWorkEventArgs e)
        {
            ((Process)e.Argument).Start();
            ((Process)e.Argument).WaitForExit();
            File.Delete(((Process)e.Argument).StartInfo.FileName);
        }
    }
}
