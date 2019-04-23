using System;
using System.Diagnostics;
using System.IO;
using Google.Protobuf;
using src.protobuf.generatedClasses;

namespace src.protobuf
{
    public class ProtobufSimulator
    {
        public void SerializarEDeserializarPayloadDeCadastroClientes(int quantity)
        {
            var cadastro = StubsProtoBuf.CriarClientes(quantity);
            var watch = Stopwatch.StartNew();
            var stream = Serialize(cadastro);
            watch.Stop();
            Console.WriteLine("RunTime " + typeof(ProtobufSimulator).Name + " Serialize a full payload of " + quantity +" Objects " + watch.Elapsed.ToFormatedString() + ", Total of bytes serialized " + stream.Length);

            watch.Restart();
            Cadastro copy = Cadastro.Parser.ParseFrom(stream);
            Console.WriteLine("RunTime " + typeof(ProtobufSimulator).Name + " Deserialize a full payload of " + quantity +" Objects " + watch.Elapsed.ToFormatedString());
        }

        public void SerializarEDeserializarPayloadsDeCliente(int quantity)
        {
            Stopwatch watch = Stopwatch.StartNew(); 
            TimeSpan tsSerialization = new TimeSpan();
            TimeSpan tsDeSerialization = new TimeSpan();
            for (int i = 0; i < quantity; i++)
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
            Console.WriteLine("RunTime " + typeof(ProtobufSimulator).Name + " time accumulated to Serialize " + quantity + " Objects " + tsSerialization.ToFormatedString());
            Console.WriteLine("RunTime " + typeof(ProtobufSimulator).Name + " time accumulated to Deserialize " + tsDeSerialization.ToFormatedString());
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