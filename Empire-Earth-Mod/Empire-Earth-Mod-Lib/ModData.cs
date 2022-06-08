using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Empire_Earth_Mod_Lib
{
    public class ModData
    {
        // Mod UUID
        public Guid Uuid { get; set; }

        // Mod Image & Banner(s)
        public Image Icon { get; set; }
        public List<Image> Banners { get; set; }

        // Mod Basic Info
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Authors { get; set; }
        public string Contact { get; set; }
        public List<string> SupportedLanguages { get; set; }
        public List<string> Variants { get; set; }
        public List<ModFile> ModFiles { get; set; }
        public List<string> RequiredMods { get; set; }
        public List<string> IncompatibleMods { get; set; }
        
        public string LicenseName { get; set; }
        public string LicenseTxt { get; set; }

        // Mod Version
        public Version Version { get; set; }
        public DateTime BuildDate { get; set; }

        // Mod EE Impact
        public bool EE { get; set; }
        public bool AoC { get; set; } 

        public WindowsVersion.WindowsVersionEnum MinWindows { get; set; }

        public ModData(string name, string description)
        {
            Name = name;
            Description = description;
            Uuid = Guid.NewGuid();
            Banners = new List<Image>();
            Authors = new List<string>();
            SupportedLanguages = new List<string>();
            Variants = new List<string>();
            ModFiles = new List<ModFile>();
            RequiredMods = new List<string>();
            IncompatibleMods = new List<string>();
        }

        public void GetImageAsync(string url)
        {
            // Since I use .NET 4 (to support WinXP) I can't download that asych...
            // So I need some background worker sh$t or idk...
        }

        public static ModData LoadFromEEM(string eemPath)
        {
            using (MemoryStream dataStream = new MemoryStream())
            {
                using (var zip = ZipStorer.Open(eemPath, FileAccess.Read))
                {
                    var entry = zip.ReadCentralDir();
                    bool valid = entry.Exists(etr => etr.FilenameInZip.Equals("data"));
                    if (!valid)
                        throw new Exception("Unable to load the EEM: data file not found!");
                    zip.ExtractFile(entry.First(etr => etr.FilenameInZip.Equals("data")), dataStream);
                }

                if (dataStream.Length == 0 && !dataStream.CanRead)
                    throw new Exception("Unable to parse the data file of EEM!");

                ModData modData = (ModData)Serializer.DeserializeFromStream(dataStream);
                return modData;
            }
        }
        
    }
}