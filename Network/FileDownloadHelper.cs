using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace EELauncher
{
    class FileDownloadHelper
    {
        private Stopwatch sw = new Stopwatch();

        object syncObject = new object();

        private Dictionary<string, string> localDownloadList;
        private List<Uri> downloadLinkList = null;
        private int downloadListIndex = 0;

        private string currentFileName = null;

        private Label label;
        private ProgressBar progressBar;
        private SynchronizationContext synchronizationContextUI;

        // This Class is to Download File Asy with UI
        public FileDownloadHelper(Label label, ProgressBar progressBar, SynchronizationContext synchronizationContextUI)
        {
            this.label = label;
            this.progressBar = progressBar;
            this.synchronizationContextUI = synchronizationContextUI;
        }

        public void DownloadOneFile(string url, string path)
        {
            url.Replace("https:", "http:");

            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                synchronizationContextUI.Post(new SendOrPostCallback((o) =>
                {
                    progressBar.Value = 0;
                    if(progressBar.Style != ProgressBarStyle.Marquee)
                        progressBar.Style = ProgressBarStyle.Marquee;
                    progressBar.Visible = true;
                }), null);

                synchronizationContextUI.Post(new SendOrPostCallback((o) =>
                {
                    label.Text = "Checking File Integrity...";
                    label.Visible = true;
                }), null);

                sw.Start();

                bool currentCheckSum = CheckSumHelper.CheckFromFileAndURL(CheckSumHelper.CheckSumAlgo.CRC, path, url);

                if (!currentCheckSum)
                {
                    if (File.Exists(path))
                        File.Delete(path);

                    if (!new FileInfo(path).Directory.Exists)
                        new FileInfo(path).Directory.Create();

                    synchronizationContextUI.Post(new SendOrPostCallback((o) =>
                    {
                        progressBar.Style = ProgressBarStyle.Blocks;
                    }), null);


                    lock (syncObject)
                    {
                        currentFileName = Path.GetFileName(path);
                        Console.WriteLine("Downloading " + currentFileName + " [" + url + "]");
                        webClient.DownloadFileAsync(new Uri(url), path);
                        //This would block the thread until download completes
                        Monitor.Wait(syncObject);
                    }

                }
                else
                {
                    Completed(null, null);
                }
            }
        }

        public void DownloadFromList(Dictionary<string, string> downloadList)
        {
            localDownloadList = new Dictionary<string, string>(downloadList.Count);
            downloadLinkList = new List<Uri>();
            downloadListIndex = 0;

            foreach (string keyCollection in downloadList.Keys)
                downloadLinkList.Add(new Uri(keyCollection.Replace("https:", "http:")));

            foreach (string keyCollection in downloadList.Keys)
                localDownloadList.Add(keyCollection.Replace("https:", "http:"), downloadList[keyCollection]);

                DownloadOneFile(downloadLinkList[downloadListIndex].AbsoluteUri,
                     localDownloadList[downloadLinkList[downloadListIndex].AbsoluteUri]);

        }

        private DateTime wanted_ui_time = DateTime.Now;
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (DateTime.Now >= wanted_ui_time)
            {
                synchronizationContextUI.Post(new SendOrPostCallback((o) =>
                {
                    label.Text = string.Format(currentFileName + "  |  {0} MB/s  |  {1} %  ({2}/{3} MB)",
                   (e.BytesReceived / 1024d / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"),
                   e.ProgressPercentage.ToString(),
                   (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                   (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
                }), null);

                synchronizationContextUI.Post(new SendOrPostCallback((o) =>
                {
                    progressBar.Value = e.ProgressPercentage;
                }), null);

                wanted_ui_time = DateTime.Now.AddMilliseconds(25);
            }

        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            // Progress Bar
            synchronizationContextUI.Post(new SendOrPostCallback((o) =>
            {
                progressBar.Visible = false;
                progressBar.Value = 0;
            }), null);

            // Label
            synchronizationContextUI.Post(new SendOrPostCallback((o) =>
            {
                label.Visible = false;
                label.Text = "";
            }), null);

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
                else
                {
                    lock (syncObject)
                    {
                        Monitor.PulseAll(syncObject);
                    }
                }
            }
            else
            {
                lock (syncObject)
                {
                    Monitor.PulseAll(syncObject);
                }
            }
        }
    }
}