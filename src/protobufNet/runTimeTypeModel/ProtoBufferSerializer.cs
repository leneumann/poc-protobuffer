using System;
using System.IO;
using ProtoBuf.Meta;

namespace src.protobufNet.runTimeTypeModel
{
public interface IProtoBufferSerializer
    {
        byte[] Serialize<T>(T obj) where T : class;
        T Deserialize<T>(byte[] data) where T : class;
    }

    public class ProtoBufferSerializer : IProtoBufferSerializer
    {
        private readonly RuntimeTypeModel _runtimeTypeModel;

        public ProtoBufferSerializer(RuntimeTypeModel runtimeTypeModel)
        {
            _runtimeTypeModel = runtimeTypeModel;
        }
        public byte[] Serialize<T>(T obj) where T : class
        {
            if (obj == null)
                return new byte[] { };
            using (var stream = new MemoryStream())
            {
                _runtimeTypeModel.Serialize(stream, obj);
                return stream.ToArray();
            }
        }
        public T Deserialize<T>(byte[] data) where T : class
        {
            var type = typeof(T);
            if (data == null)
                return default(T);
            using (var stream = new MemoryStream(data))
            {
                return (T) _runtimeTypeModel.Deserialize(stream, null, type);
            }
        }
    }
}