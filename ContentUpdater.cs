using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EELauncher
{
    class ContentUpdater
    {

        enum GameContent
        {
            Lang, Intro, Data
        }

        private GameHelper gameHelper;

        private Label label;
        private ProgressBar progressBar;
        private SynchronizationContext synchronizationContextUI;

        public ContentUpdater(GameHelper gameHelper, Label label, ProgressBar progressBar, SynchronizationContext synchronizationContextUI)
        {
            this.gameHelper = gameHelper;
            this.label = label;
            this.progressBar = progressBar;
            this.synchronizationContextUI = synchronizationContextUI;
        }

        // Download lang.dll
        public void UpdateLang(string lang)
        {
            // Copy from repo to game folder if needed
            bool langRemote = CheckSumHelper.CheckFromFileAndURL(CheckSumHelper.CheckSumAlgo.CRC,
                gameHelper.GetInstallFolder().FullName + @"\Language.dll",
                "http://storage.empireearth.eu/EE/Languages/" + lang + "/Language.dll");

            if (!langRemote)
            {
                new FileDownloadHelper(label, progressBar, synchronizationContextUI).DownloadOneFile("http://storage.empireearth.eu/EE/Languages/" + lang + "/Language.dll",
                    "./Languages/" + lang + "/Language.dll");
            }

            string langProgram = CheckSumHelper.GetFromFile(CheckSumHelper.CheckSumAlgo.CRC, gameHelper.GetInstallFolder().FullName + @"\Language.dll");
            string langLocal = CheckSumHelper.GetFromFile(CheckSumHelper.CheckSumAlgo.CRC, "./Languages/" + lang + "/Language.dll");

            if (!langLocal.Equals(langProgram))
            {
                File.Copy("./Languages/" + lang + "/Language.dll", gameHelper.GetInstallFolder().FullName + @"/Language.dll", true);
            }
        }

        // Only copy from local repo to game
        public void UpdateLangOffline(string lang)
        {
            // Copy from repo to game folder if needed
            string langProgram = CheckSumHelper.GetFromFile(CheckSumHelper.CheckSumAlgo.CRC, gameHelper.GetInstallFolder().FullName + @"\Language.dll");
            string langLocal = CheckSumHelper.GetFromFile(CheckSumHelper.CheckSumAlgo.CRC, "./Languages/" + lang + "/Language.dll");
            if (!langLocal.Equals(langProgram))
            {
                File.Copy("./Languages/" + lang + "/Language.dll", gameHelper.GetInstallFolder().FullName + @"/Language.dll", true);
            }
        }

        // Download EE.bik
        public void UpdateIntro(bool skipIntro, string lang)
        {
            DirectoryInfo moviesDirectoryInfo = new DirectoryInfo(gameHelper.GetInstallFolder().FullName + @"\Data\Movies");
            DirectoryInfo moviesDisabledDirectoryInfo = new DirectoryInfo(gameHelper.GetInstallFolder().FullName + @"\Data\Movies_");
            try
            {
                if (skipIntro)
                {
                    if (moviesDirectoryInfo.Exists)
                    {
                        moviesDirectoryInfo.MoveTo(moviesDisabledDirectoryInfo.FullName);
                    }
                }
                else
                {
                    if (moviesDisabledDirectoryInfo.Exists)
                    {
                        moviesDisabledDirectoryInfo.MoveTo(moviesDirectoryInfo.FullName);
                    }

                    new FileDownloadHelper(label, progressBar, synchronizationContextUI)
                        .DownloadOneFile("http://storage.empireearth.eu/EE/Languages/" + lang + "/Data/Movies/Empire%20Earth.bik",
                        gameHelper.GetInstallFolder().FullName + @"Data\Movies\Empire Earth.bik");


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unnable to rename Movies folder !\nIs the game already running ?\n Error : " + ex.Message);
            }
        }

        public void UpdateData(string lang)
        {
            new FileDownloadHelper(label, progressBar, synchronizationContextUI)
                .DownloadOneFile("http://storage.empireearth.eu/EE/Languages/" + lang + "/Data/data.ssa",
                    gameHelper.GetInstallFolder().FullName + "/Data/data.ssa");
        }

        public void UpdateDrex(bool enabled)
        {
            if (enabled)
            {

            }
        }

    }
}
