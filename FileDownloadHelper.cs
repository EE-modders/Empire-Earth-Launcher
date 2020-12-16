using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EELauncher
{
    class FileDownloadHelper
    {
        private Stopwatch sw = new Stopwatch();

        private Dictionary<string, string> localDownloadList = new Dictionary<string, string>();
        private List<Uri> downloadLinkList = null;
        private int downloadListIndex = 0;

        private Label label;
        private ProgressBar progressBar;


        // Dirty var
        private bool currentCheckSum = false;
        private string currentUrl = null;
        private string currentPath = null;

        // This Class is to Download File Asy with UI
        public FileDownloadHelper(Label label, ProgressBar progressBar)
        {
            this.label = label;
            this.progressBar = progressBar;
        }

        public void DownloadOneFile(string url, string path)
        {
            url.Replace("https:", "http:");

            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                progressBar.Value = 0;
                progressBar.Style = ProgressBarStyle.Marquee;
                progressBar.Visible = true;
                label.Text = "Checking File Integrity...";
                label.Visible = true;
                sw.Start();

                BackgroundWorker installBackgroundWorker = new BackgroundWorker();
                installBackgroundWorker.DoWork += CheckSumWork;
                installBackgroundWorker.RunWorkerCompleted += CheckSumWorkComplete;

                try
                {
                    currentUrl = url;
                    currentPath = path;
                    installBackgroundWorker.RunWorkerAsync(webClient);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unnable to download : " + "N/A"
                            + "Error : " + ex.Message, "Download Error !");
                }
            }
        }

        private void CheckSumWorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!currentCheckSum)
            {
                if (File.Exists(currentPath))
                    File.Delete(currentPath);

                if (!new FileInfo(currentPath).Directory.Exists)
                    new FileInfo(currentPath).Directory.Create();

                progressBar.Style = ProgressBarStyle.Blocks;
                ((WebClient)e.Result).DownloadFileAsync(new Uri(currentUrl), currentPath);
            }
            else
            {
                Completed(null, null);
            }
        }

        private void CheckSumWork(object sender, DoWorkEventArgs e)
        {
            currentCheckSum = CheckSumHelper.CheckFromFileAndURL(CheckSumHelper.CheckSumAlgo.CRC, currentPath, currentUrl);
            e.Result = e.Argument;
        }

        public void DownloadFromList(Dictionary<string, string> downloadList)
        {
            localDownloadList = downloadList;
            downloadLinkList = new List<Uri>();
            downloadListIndex = 0;

            foreach (string keyCollection in localDownloadList.Keys)
                downloadLinkList.Add(new Uri(keyCollection.Replace("https:", "http:")));

            DownloadOneFile(downloadLinkList[downloadListIndex].AbsoluteUri,
                     localDownloadList[downloadLinkList[downloadListIndex].AbsoluteUri]);

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
            progressBar.Visible = false;
            progressBar.Value = 0;
            label.Visible = false;
            sw.Reset();

            // Check more download to do
            if (downloadLinkList != null)
            {
                if (downloadLinkList.Count != downloadListIndex + 1)
                {
                    downloadListIndex++;
                    DownloadOneFile(downloadLinkList[downloadListIndex].AbsoluteUri,
                        localDownloadList[downloadLinkList[downloadListIndex].AbsoluteUri]);
                }
            }
        }
    }
}