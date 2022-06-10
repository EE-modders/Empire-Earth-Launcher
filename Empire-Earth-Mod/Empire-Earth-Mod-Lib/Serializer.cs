using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;

// ReSharper disable once CheckNamespace
namespace Empire_Earth_Mod_Lib.Serialization
{
    public class BinarySerializer <TType> where TType : class
    {
        public static MemoryStream Serialize(TType instance)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, instance);
            return stream;
        }

        public static TType Deserialize(MemoryStream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            var o = formatter.Deserialize(stream);
            return o as TType;
        }
    }

    public class JsonSerializer<TType> where TType : class
    {
        public static string Serialize(TType instance, bool base64 = false)
        {
            var serializer = new DataContractJsonSerializer(typeof(TType));
            using var stream = new MemoryStream();
            serializer.WriteObject(stream, instance);
            return base64
                ? Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(stream.ToArray())))
                : Encoding.UTF8.GetString(stream.ToArray());
        }
        
        public static TType Deserialize(string json, bool base64 = false)
        {
            byte[] decoded = Encoding.UTF8.GetBytes(
                base64 ? Encoding.UTF8.GetString(Convert.FromBase64String(json)) : json
            );
            using var stream = new MemoryStream(decoded);
            var serializer = new DataContractJsonSerializer(typeof(TType));
            return serializer.ReadObject(stream) as TType;
        }
        
        public static TType Deserialize(MemoryStream streamReader, bool base64 = false)
        {
            byte[] decoded = Encoding.UTF8.GetBytes(
                base64 ? Encoding.UTF8.GetString(
                    Convert.FromBase64String(new StreamReader(streamReader).ReadToEnd())) 
                    : new StreamReader(streamReader).ReadToEnd()
            );
            using var stream = new MemoryStream(decoded);
            var serializer = new DataContractJsonSerializer(typeof(TType));
            return serializer.ReadObject(stream) as TType;
        }
    }
}