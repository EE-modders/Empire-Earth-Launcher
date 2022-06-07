namespace Empire_Earth_Mod_Lib
{
    public class ModFile
    {
        public string RelativeFilePath { get; set; }
        public bool Edit { get; set; }
        public bool Remove { get; set; }
        public bool Backup { get; set; }
        public string CRC32 { get; set; }

        public ModFile(string relativeFilePath, bool edit, bool remove, bool backup, string crc32)
        {
            this.Edit = edit;
            this.Remove = remove;
            this.Backup = backup;
            this.CRC32 = crc32;
            this.RelativeFilePath = relativeFilePath;
        }
    }
}