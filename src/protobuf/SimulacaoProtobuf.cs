using System;
using System.Diagnostics;
using System.IO;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using src.protobuf.generatedClasses;

namespace src.protobuf
{
    public class SimulacaoProtobuf
    {
        public void SerializarEDeserializarPayloadDeCadastroClientes(int times)
        {
            var cadastro = StubsProtoBuf.CriarClientes(times);
            byte[] bytes;
            var watchSerialize = Stopwatch.StartNew();
            bytes = Serialize(cadastro);
            watchSerialize.Stop();
            TimeSpan tsSerialize = watchSerialize.Elapsed;
            string elapsedTimeSerialize = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", tsSerialize.Hours, tsSerialize.Minutes, tsSerialize.Seconds, tsSerialize.Milliseconds);
            Console.WriteLine("RunTime ProtoBuffer SErialize " + elapsedTimeSerialize + ", Total bytes per serialization " + bytes.Length);

            var watchDeserialize = Stopwatch.StartNew();
            Cadastro copy = Cadastro.Parser.ParseFrom(bytes);
            TimeSpan tsDeserialize = watchDeserialize.Elapsed;
            string elapsedTimeDeserialize = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", tsDeserialize.Hours, tsDeserialize.Minutes, tsDeserialize.Seconds, tsDeserialize.Milliseconds);
            Console.WriteLine("RunTime ProtoBuffer DEserialize " + elapsedTimeDeserialize);
        }

        public void SerializarEDeserializarPayloadsDeCliente(int times)
        {
            Stopwatch watch = null;
            TimeSpan ts = new TimeSpan();
            for (int i = 0; i < times; i++)
            {
                var cliente = StubsProtoBuf.CriarCliente();
                watch = Stopwatch.StartNew();
                var stream = Serialize(cliente);
                watch.Stop();
                ts += watch.Elapsed;
            }
            string elapsedTimeSerialize = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("RunTime ProtoBuffer SErialize " + elapsedTimeSerialize + ", Total bytes serializing "+ times + " objects");
        }

        public byte[] Serialize(IMessage message)
        {
            byte[] bytes;
            using (MemoryStream stream = new MemoryStream())
            {
                message.WriteTo(stream);
                bytes = stream.ToArray();
            }
            return bytes;
        }
    }
}