using System;
using System.Diagnostics;
using System.IO;
using Google.Protobuf;
using src.protobuf.generatedClasses;

namespace src.protobuf
{
    public class ProtobufSimulator
    {
        public void SerializarEDeserializarPayloadDeCadastroClientes(int times)
        {
            var cadastro = StubsProtoBuf.CriarClientes(times);
            var watch = Stopwatch.StartNew();
            var stream = Serialize(cadastro);
            watch.Stop();
            Console.WriteLine("RunTime " + typeof(ProtobufSimulator).Name + " SErialize " + watch.Elapsed.ToFormatedString() + ", Total bytes per serialization " + stream.Length);

            watch.Restart();
            Cadastro copy = Cadastro.Parser.ParseFrom(stream);
            Console.WriteLine("RunTime " + typeof(ProtobufSimulator).Name + " DEserialize " + watch.Elapsed.ToFormatedString());
        }

        public void SerializarEDeserializarPayloadsDeCliente(int times)
        {
            Stopwatch watch = Stopwatch.StartNew(); 
            TimeSpan tsSerialization = new TimeSpan();
            TimeSpan tsDeSerialization = new TimeSpan();
            for (int i = 0; i < times; i++)
            {
                var cliente = StubsProtoBuf.CriarCliente();
                var stream = Serialize(cliente);
                watch.Stop();
                tsSerialization += watch.Elapsed;
                watch.Restart();
                Cadastro.Types.Cliente copy = Cadastro.Types.Cliente.Parser.ParseFrom(stream);
                tsDeSerialization += watch.Elapsed;
                watch.Restart();
            }
            Console.WriteLine("RunTime " + typeof(ProtobufSimulator).Name + " time accumulated to SErialize " + times + " Objects " + tsSerialization.ToFormatedString());
            Console.WriteLine("RunTime " + typeof(ProtobufSimulator).Name + " time accumulated to DeSErialize " + tsDeSerialization.ToFormatedString());
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