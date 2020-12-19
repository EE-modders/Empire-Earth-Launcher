using System;
using System.IO;
using System.Net;

namespace EELauncher
{
    class CheckSumHelper
    {
        public enum CheckSumAlgo
        {
            CRC
        }

        public static bool CheckFromFileAndURL(CheckSumAlgo algo, string path, string url)
        {
            Crc32 crc32 = new Crc32();
            string hashLocal = string.Empty;
            string hashOnline = string.Empty;


            if (!new FileInfo(path).Exists)
                return false;

            try
            {
                using (FileStream fs = File.Open(new FileInfo(path).FullName, FileMode.Open))
                    foreach (byte b in crc32.ComputeHash(fs))
                        hashLocal += b.ToString("x2").ToLower();
            }
            catch
            {
                return false;
            }

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    hashOnline = webClient.DownloadString(
                        url.Replace("https:", "http:").
                        Replace(Path.GetFileName(path).Replace(" ", "%20"),
                        Path.GetFileNameWithoutExtension(path).Replace(" ", "%20") + ".crc32")).ToLower();
                }
            }
            catch
            {
                Console.WriteLine("CheckSum Fail For " + url.Replace("https:", "http:").
                        Replace(Path.GetFileName(path).Replace(" ", "%20"),
                        Path.GetFileNameWithoutExtension(path).Replace(" ", "%20") + ".crc32"));
                return false;
            }

            Console.WriteLine(Path.GetFileName(path) + " | " + hashLocal + " (local) | (online) " + hashOnline);

            if (hashLocal.Equals(hashOnline) &&
                !string.IsNullOrEmpty(hashLocal) &&
                !string.IsNullOrEmpty(hashOnline))
            {
                return true;
            }

            return false;
        }

        public static string GetFromFile(CheckSumAlgo algo, string path)
        {
            Crc32 crc32 = new Crc32();
            string hashLocal = string.Empty;

            if (!new FileInfo(path).Exists)
            {
                return null;
            }

            try
            {
                using (FileStream fs = File.Open(new FileInfo(path).FullName, FileMode.Open))
                {
                    foreach (byte b in crc32.ComputeHash(fs))
                    {
                        hashLocal += b.ToString("x2").ToLower();
                    }
                }
            }
            catch
            {
                return null;
            }

            return hashLocal;
        }

    }
}
