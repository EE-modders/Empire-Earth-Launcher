using System.IO;
using System.Net;
using System.Windows.Forms;

namespace EELauncher
{
    class ContentUpdater
    {
        private GameHelper gameHelper;
        private int retry_remaining = 5;
        public ContentUpdater(GameHelper gameHelper)
        {
            this.gameHelper = gameHelper;
        }

        public void UpdateLang(string lang)
        {
            if (!Directory.Exists("./Languages/" + lang))
            {
                Directory.CreateDirectory("./Languages/" + lang);
            }

            // Check local repo
            if (!CheckSumHelper.CheckFromFileAndURL(CheckSumHelper.CheckSumAlgo.CRC, "./Languages/" + lang + "/Language.dll",
                "http://storage.empireearth.eu/" + gameHelper.GetGameType().ToString().ToUpper() + "/Languages/" + lang + "/Language.crc32"))
            {
                new WebClient().DownloadFile("http://storage.empireearth.eu/EE/Languages/" + lang + "/Language.dll", "./Languages/" + lang + "/Language.dll");

                // ReCheck CRC-32 & Retry if possible
                if (!CheckSumHelper.CheckFromFileAndURL(CheckSumHelper.CheckSumAlgo.CRC, "./Languages/" + lang + "/Language.dll",
                "http://storage.empireearth.eu/" + gameHelper.GetGameType().ToString().ToUpper() + "/Languages/" + lang + "/Language.crc32"))
                {
                    if (CheckAndRegisterRetry())
                    {
                        UpdateLang(lang);
                    }
                    else
                    {
                        return;
                    }
                }
            }

            // Copy from repo to game folder if needed
            string langProgram = CheckSumHelper.GetFromFile(CheckSumHelper.CheckSumAlgo.CRC, gameHelper.GetInstallFolder().FullName + @"\Language.dll");
            string langLocal = CheckSumHelper.GetFromFile(CheckSumHelper.CheckSumAlgo.CRC, "./Languages/" + lang + "/Language.dll");
            if (!langLocal.Equals(langProgram))
            {
                File.Copy("./Languages/" + lang + "/Language.dll", gameHelper.GetInstallFolder().FullName + @"Language.dll", true);
            }
        }

        private bool CheckAndRegisterRetry()
        {
            if (retry_remaining <= 0)
            {
                MessageBox.Show("Too many error while checking update !\n" +
                    "Maybe an server side problem or your launcher isn't up-to-date !\n" +
                    "Sorry for the problem !");
                return false;
            }
            else
            {
                retry_remaining--;
                return true;
            }
        }

    }
}
