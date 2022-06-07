using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

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
        public List<ModFile> ModFiles;
        public List<string> RequiredMods;
        public List<string> IncompatibleMods;
        
        public string LicenseName { get; set; }
        public string LicenseTxt { get; set; }

        // Mod Version
        public Version Version { get; set; }
        public DateTime BuildDate { get; set; }

        // Mod EE Impact
        public bool EE { get; set; }
        public bool AoC { get; set; }

        public WindowsVersionHelper MinWindows { get; set; }

        public ModData(){}

        public async Task<Image> GetImageAsync(string url)
        {
            var tcs = new TaskCompletionSource<Image>();
            Image webImage = null;
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "GET";
            await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null)
                .ContinueWith(task =>
                {
                    var webResponse = (HttpWebResponse) task.Result;
                    Stream responseStream = webResponse.GetResponseStream();
                    if (responseStream == null)
                        return;
                    if (webResponse.ContentEncoding.ToLower().Contains("gzip"))
                        responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                    else if (webResponse.ContentEncoding.ToLower().Contains("deflate"))
                        responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);
                    webImage = Image.FromStream(responseStream);
                    tcs.TrySetResult(webImage);
                    webResponse.Close();
                    responseStream.Close();
                });
            return tcs.Task.Result;
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