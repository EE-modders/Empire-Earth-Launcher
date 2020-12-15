using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace EELauncher
{
    class LanguagesHelper
    {

        private string url;
        private ComboBox comboBox;

        public LanguagesHelper(string url)
        {
            this.url = url;
        }

        public void PopList(ComboBox comboBox)
        {
            this.comboBox = comboBox;
            BackgroundWorker connectionBackgroundWorker = new BackgroundWorker();
            connectionBackgroundWorker.DoWork += LanguagesHelperBackgroundWorker_DoWork;
            connectionBackgroundWorker.RunWorkerCompleted += LanguagesHelperBackgroundWorker_RunWorkerCompleted;
            connectionBackgroundWorker.RunWorkerAsync();
        }

        private void LanguagesHelperBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Online Pop
            if (!string.IsNullOrEmpty((string)e.Result))
            {
                string[] langs = ((string)e.Result).Split(new char[] { ',' });
                foreach (string lang in langs)
                {
                    if (!comboBox.Items.Contains(lang))
                    {
                        comboBox.Items.Add(lang);
                    }
                }
            }
        }

        private void LanguagesHelperBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DirectoryInfo languagesDirectoryInfo = new DirectoryInfo("./Languages");
            List<string> localdata = new List<string>();

            // Local Pop
            if (!languagesDirectoryInfo.Exists)
            {
                languagesDirectoryInfo.Create();
            }

            foreach (DirectoryInfo fileInfo in languagesDirectoryInfo.GetDirectories())
            {
                localdata.Add(fileInfo.Name);
            }

            if (localdata.Count > 1)
            {
                e.Result = string.Join(",", localdata.ToArray());
            }
            else if (localdata.Count == 1)
            {
                e.Result = localdata[0] + ',';
            }

            try
            {
                WebClient client = new WebClient();
                e.Result += client.DownloadString(url);
                client.Dispose();
            }
            catch
            {
                e.Result = null;
            };
        }
    }
}
