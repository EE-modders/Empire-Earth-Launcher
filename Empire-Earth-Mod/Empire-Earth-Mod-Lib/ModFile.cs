using System.ComponentModel;

namespace Empire_Earth_Mod_Lib
{
    public class ModFile
    {
        public string RelativeFilePath { get; set; }
        public bool Remove { get; set; }
        public ModFileType Type { get; set; }
        public string CRC32 { get; set; }
        
        public enum ModFileType
        {
            [Description("Data")] Data = 0,
            [Description("Config File")] ConfigFile = 1,
            [Description("Executable")] Executable = 2
        }

        public ModFile(string relativeFilePath, bool remove, ModFileType type, string crc32)
        {
            this.Remove = remove;
            this.Type = type;
            this.CRC32 = crc32;
            this.RelativeFilePath = relativeFilePath;
        }
    }
}