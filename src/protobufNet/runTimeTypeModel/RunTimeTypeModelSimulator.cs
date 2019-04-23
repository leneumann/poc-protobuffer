using System;
using System.Diagnostics;
using ProtoBuf.Meta;

namespace src.protobufNet.runTimeTypeModel
{
    public class RunTimeTypeModelSimulator
    {
        private readonly RuntimeTypeModel _proto;
        public RunTimeTypeModelSimulator()
        {
            _proto = ProtoMapBuilder.New()
            .MapObject<Cadastro>()
            .MapAllProperties()
            .MapObject<Cliente>()
            .MapAllProperties()
            .MapObject<Endereco>()
            .MapAllProperties()
            .Build();
        }
        public void SerializarEDeserializarPayloadDeCadastroClientes(int quantity)
        {
            int lengthOf = 0;
            IProtoBufferSerializer Serializer = new ProtoBufferSerializer(_proto);
            var cadastro = StubsRunTimeTypeModel.CriarCadastro(quantity);
            Stopwatch watch = Stopwatch.StartNew();
            var stream = Serializer.Serialize<Cadastro>(cadastro);
            watch.Stop();
            lengthOf = stream.Length;
            Console.WriteLine("RunTime " + typeof(RunTimeTypeModelSimulator).Name + " Serialize a full payload of " + quantity +" Objects " + watch.Elapsed.ToFormatedString() + ", Total of bytes serialized " + lengthOf);
            watch.Restart();
            cadastro = Serializer.Deserialize<Cadastro>(stream);
            watch.Stop();
            Console.WriteLine("RunTime " + typeof(RunTimeTypeModelSimulator).Name + " Deserialize a full payload of " + quantity +" Objects " + watch.Elapsed.ToFormatedString());
        }

        public void SerializarEDeserializarPayloadsDeCliente(int times)
        {
            IProtoBufferSerializer Serializer = new ProtoBufferSerializer(_proto);

            Stopwatch watch = Stopwatch.StartNew();
            TimeSpan tsSerialization = new TimeSpan();
            TimeSpan tsDeSerialization = new TimeSpan();
            for (int i = 0; i < times; i++)
            {
                var cliente = StubsRunTimeTypeModel.CriarCliente();
                var stream = Serializer.Serialize<Cliente>(cliente);
                watch.Stop();
                tsSerialization += watch.Elapsed;
                watch.Restart();
                cliente = Serializer.Deserialize<Cliente>(stream);
                tsDeSerialization += watch.Elapsed;
                watch.Restart();
            }
            Console.WriteLine("RunTime " + typeof(RunTimeTypeModelSimulator).Name + " time accumulated to Serialize " + times + " Objects " + tsSerialization.ToFormatedString());
            Console.WriteLine("RunTime " + typeof(RunTimeTypeModelSimulator).Name + " time accumulated to Deserialize " + tsDeSerialization.ToFormatedString());
        }
    }
}