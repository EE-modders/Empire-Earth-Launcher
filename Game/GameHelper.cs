using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace EELauncher
{
    public class GameHelper
    {
        public enum GameProvider
        {
            Official, Neo //, Reborn
        }
        public enum GameType
        {
            EE, AoC
        }

        GameProvider gameProvider;
        GameType gameType;

        List<string> paths = new List<string>();
        string setup_url;

        public GameHelper(GameType gameType, GameProvider gameProvider)
        {
            this.gameType = gameType;
            this.gameProvider = gameProvider;

            switch (gameType)
            {
                case GameType.EE:
                    if (gameProvider == GameProvider.Neo)
                    {
                        paths.Add((string)ReadReg(Registry.CurrentUser,
                            @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{30966ED6-5708-45C1-8B77-1A8BDBCF3570}_is1", "InstallLocation"));
                        paths.Add((string)ReadReg(Registry.LocalMachine,
                            @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{30966ED6-5708-45C1-8B77-1A8BDBCF3570}_is1", "InstallLocation"));
                        setup_url = "http://storage.empireearth.eu/Setup/NeoEE_EE.exe";
                    }
                    break;
                default:
                    MessageBox.Show("Invalid GameID !");
                    break;
            }

        }

        public GameProvider GetProvider()
        {
            return gameProvider;
        }

        public GameType GetGameType()
        {
            return gameType;
        }

        public string GetSetupURL()
        {
            return setup_url;
        }

        public string GetName()
        {
            switch (gameType)
            {
                case GameType.EE:
                    if (gameProvider == GameProvider.Neo)
                    {
                        return "NeoEE";
                    }
                    else
                    {
                        return "Empire Earth";
                    }

                case GameType.AoC:
                    if (gameProvider == GameProvider.Neo)
                    {
                        return "NeoEE AoC";
                    }
                    else
                    {
                        return "Empire Earth AoC";
                    }

                default:
                    return null;
            }
        }

        public DirectoryInfo GetInstallFolder()
        {
            foreach (string val in paths)
            {
                if (!string.IsNullOrEmpty(val))
                {
                    if (new DirectoryInfo(val).Exists)
                    {
                        return new DirectoryInfo(val);
                    }
                }
            }

            return null;
        }

        public bool IsInstalled()
        {
            int found = 0;
            foreach (string val in paths)
            {
                if (!string.IsNullOrEmpty(val))
                {
                    if (new DirectoryInfo(val).Exists)
                    {
                        found++;
                    }
                }
            }

            if (found >= 1)
            {
                return true;
            }

            return false;
        }

        public bool MultipleInstall()
        {
            int found = 0;
            foreach (string val in paths)
            {
                if (!string.IsNullOrEmpty(val))
                {
                    found++;
                }
            }

            if (found > 1)
            {
                return true;
            }

            return false;
        }

        public bool NeedAdmin()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(GetInstallFolder() + "/write_test");
            if (!directoryInfo.Parent.Exists)
            {
                return false;
            }

            try
            {
                if (directoryInfo.Exists)
                {
                    directoryInfo.Delete(true);
                }

                directoryInfo.Create();
                directoryInfo.Delete(true);
                return false;
            }
            catch
            {
                return true;
            }
        }

        private string ReadReg(RegistryKey user_or_machine, string path, string val)
        {
            string res = null;
            try
            {
                using (RegistryKey key = user_or_machine.OpenSubKey(path))
                {
                    if (key?.GetValue(val) is string configuredPath)
                    {
                        res = configuredPath;
                    }
                }
            }
            catch (NullReferenceException) { }

            return res;
        }

        public void Start()
        {
            Process process = new Process();
            process.StartInfo.WorkingDirectory = GetInstallFolder().FullName;
            switch (gameType)
            {
                case GameType.EE:
                    process.StartInfo.FileName = GetInstallFolder().FullName + @"\Empire Earth.exe";
                    process.Start();
                    break;
                case GameType.AoC:
                    process.StartInfo.FileName = GetInstallFolder().FullName + @"\EE-AoC.exe";
                    process.Start();
                    break;
                default:
                    MessageBox.Show("Unknow game ! Unnable to start the game !");
                    break;
            }
        }

        public void Install(LauncherForm launcherForm, ProgressBar progressBar, Label label)
        {

            // Block UI & Reset ProgressBar
            //launcherForm.Enabled = false;
            progressBar.Value = 0;
            progressBar.Visible = true;
            label.Visible = true;


            new GameDownloadHelper(this, launcherForm, progressBar, label, "./" + Path.GetFileName(setup_url), true).DownloadFile();

        }

    }
}
