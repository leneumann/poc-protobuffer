using System.IO;

namespace src.protobufNet.annotations
{
    public class Serializer
    {
         public static byte[] Serialize<T>(T obj) where T : class
       {
           if (obj == null)
               return new byte[] { };
           using(var stream = new MemoryStream())
           {
               ProtoBuf.Serializer.Serialize<T>(stream, obj);
               return stream.ToArray();
           }
       }
       public static T Deserialize<T>(byte[] data) where T : class
       {
           if (data == null)
               return default(T);
           using (var stream = new MemoryStream(data))
           {
               return ProtoBuf.Serializer.Deserialize<T>(stream);
           }
       }
    }
}