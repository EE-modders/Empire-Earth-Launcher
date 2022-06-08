using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Empire_Earth_Mod_Lib
{
    public class ModManager
    {

        private DirectoryInfo ModWorkDirectory;
        private DirectoryInfo EEC_GameDirectory;
        private DirectoryInfo AOC_GameDirectory;

        private List<ModData> _modDatas;

        public ModManager(string eecGameDirectory, string aocGameDirectory, string modWorkDirectory)
        {
            EEC_GameDirectory = new DirectoryInfo(eecGameDirectory);
            AOC_GameDirectory = new DirectoryInfo(aocGameDirectory);
            ModWorkDirectory = new DirectoryInfo(modWorkDirectory);
            if (!ModWorkDirectory.Exists || !EEC_GameDirectory.Exists)
                throw new DirectoryNotFoundException("Unable to find the mod work dir or eec dir!");
        }

        public bool Init()
        {
            foreach (FileInfo fi in ModWorkDirectory.GetFiles("*.eem"))
            {
                _modDatas.Add(ModData.LoadFromEEM(fi.FullName));
            }
            return true;
        }

        public void Install(FileInfo fileInfo)
        {
            
        }
        
        public void Install(Guid uuid)
        {
            
        }
        
        public void Uninstall(string uuid)
        {
            
        }
        
    }
}