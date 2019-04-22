using System;
using System.Diagnostics;

namespace src.protobufNet.runTimeTypeModel
{
    public class RunTimeTypeModelSimulator
    {
        public void SerializarEDeserializarPayloadDeCadastroClientes(int times)
        {
            var proto = ProtoMapBuilder.New()
            .MapObject<Cadastro>()
            .MapProperty(cadastro => cadastro.clientes)
            .MapObject<Cliente>()
            .MapProperty(cliente => cliente.PrimeroNome)
            .MapProperty(cliente => cliente.UltimoNome)
            .MapProperty(cliente => cliente.DataNascimento)
            .MapProperty(cliente => cliente.Enderecos)
            .MapObject<Endereco>()
            .MapProperty(endereco => endereco.Logradouro)
            .MapProperty(endereco => endereco.Complemento)
            .MapProperty(endereco => endereco.Bairro)
            .MapProperty(endereco => endereco.Cidade)
            .MapProperty(endereco => endereco.Pais)
            .Build();

            IProtoBufferSerializer Serializer = new ProtoBufferSerializer(proto);

            Stopwatch watch = null;
            TimeSpan ts = new TimeSpan();
            for (int i = 0; i < times; i++)
            {
                var cliente = StubsRunTimeTypeModel.CriarCliente();
                watch = Stopwatch.StartNew();
                var stream = Serializer.Serialize<Cliente>(cliente);
                cliente = Serializer.Deserialize<Cliente>(stream);
                watch.Stop();
                ts += watch.Elapsed;
            }
            string elapsedTimeSerialize = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("RunTime Proto.net SErialize " + elapsedTimeSerialize + ", Total bytes serializing " + times + " objects");
        }


        public void simularJson(int times)
        {
            string json = null;
            int lengthOf = 0;
            var cadastro = StubsRunTimeTypeModel.CriarCadastro(times);
            var watch = Stopwatch.StartNew();
            json = Newtonsoft.Json.JsonConvert.SerializeObject(cadastro);
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            lengthOf = System.Text.ASCIIEncoding.Unicode.GetByteCount(json);
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("RunTime Json Serialize " + elapsedTime + ", Total bytes per serialization " + lengthOf);

            watch = Stopwatch.StartNew();
            cadastro = Newtonsoft.Json.JsonConvert.DeserializeObject<Cadastro>(json);
            watch.Stop();
            ts = watch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("RunTime Json Deserialize " + elapsedTime + ", Total bytes per serialization");
        }
    }
}