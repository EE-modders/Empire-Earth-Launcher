using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;

// ReSharper disable once CheckNamespace
namespace Empire_Earth_Mod_Lib.Serialization
{
    public class BinarySerializer
    {
        public static MemoryStream Serialize(object o)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, o);
            return stream;
        }

        public static object Deserialize(MemoryStream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            var o = formatter.Deserialize(stream);
            return o;
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