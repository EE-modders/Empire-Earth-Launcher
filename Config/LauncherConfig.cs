namespace EELauncher
{
    public class LauncherConfig
    {
        private IniFile config;
        private string section = "EELauncher";

        public LauncherConfig(IniFile config)
        {
            this.config = config;
        }

        // Create everything if needed
        public void Init()
        {
            CreateIfNotExist("Language", "english");
            CreateIfNotExist("Menu", "1280x720");
            CreateIfNotExist("dgVoodoo", "Disabled");
            CreateIfNotExist("dreXmod", "true");
            CreateIfNotExist("Discord", "false");
            CreateIfNotExist("Reborn", "false");
            CreateIfNotExist("Neo", "true");
            CreateIfNotExist("AoC", "false");
            CreateIfNotExist("SkipIntro", "false");
            CreateIfNotExist("FullGame", "true");
            CreateIfNotExist("Help", "true");
        }

        private void CreateIfNotExist(string entry, string data)
        {
            if (string.IsNullOrEmpty(config.Read(entry, section)))
                config.Write(entry, data, section);
        }

        public string GetLanguage()
        {
            return config.Read("Language", section);
        }

        public void SetLanguage(string lang)
        {
            config.Write("Language", lang);
        }

        public string GetMenu()
        {
            return config.Read("Menu", section);
        }

        public void SetMenu(string menu)
        {
            config.Write("Menu", menu);
        }

        public string GetDgVoodoo()
        {
            return config.Read("dgVoodoo", section);
        }

        public void SetDgVoodoo(string dgVoodoo)
        {
            config.Write("dgVoodoo", dgVoodoo);
        }

        public bool GetDreXmod()
        {
            return bool.Parse(config.Read("dreXmod", section));
        }

        public void SetDreXmod(bool dreXmod)
        {
            config.Write("dreXmod", dreXmod.ToString());
        }

        public bool GetDiscord()
        {
            return bool.Parse(config.Read("Discord", section));
        }

        public void SetDiscord(bool discord)
        {
            config.Write("Discord", discord.ToString());
        }

        public bool GetReborn()
        {
            return bool.Parse(config.Read("Reborn", section));
        }

        public void SetReborn(bool reborn)
        {
            config.Write("Reborn", reborn.ToString());
        }

        public bool GetNeo()
        {
            return bool.Parse(config.Read("Neo", section));
        }

        public void SetNeo(bool neo)
        {
            config.Write("Neo", neo.ToString());
        }

        public bool GetAoC()
        {
            return bool.Parse(config.Read("AoC", section));
        }

        public void SetAoC(bool aoc)
        {
            config.Write("AoC", aoc.ToString());
        }

        public bool GetSkipIntro()
        {
            return bool.Parse(config.Read("SkipIntro", section));
        }

        public void SetSkipIntro(bool skipintro)
        {
            config.Write("SkipIntro", skipintro.ToString());
        }

        public bool GetFullGame()
        {
            return bool.Parse(config.Read("FullGame", section));
        }

        public void SetFullGame(bool fullgame)
        {
            config.Write("FullGame", fullgame.ToString());
        }

        public bool GetHelp()
        {
            return bool.Parse(config.Read("Help", section));
        }

        public void SetHelp(bool help)
        {
            config.Write("Help", help.ToString());
        }
    }
}
