using System;
using System.Diagnostics;

namespace src.json
{
    public class JsonSimulator
    {
        public void SerializarEDeserializarPayloadDeCadastroClientes(int quantity)
        {
            string json = null;
            int lengthOf = 0;
            var cadastro = protobufNet.runTimeTypeModel.StubsRunTimeTypeModel.CriarCadastro(quantity);
            var watch = Stopwatch.StartNew();
            json = Newtonsoft.Json.JsonConvert.SerializeObject(cadastro);
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            lengthOf = System.Text.ASCIIEncoding.Unicode.GetByteCount(json);
            Console.WriteLine("RunTime " + typeof(JsonSimulator).Name + " Serialize a full payload of " + quantity +" Objects " + ts.ToFormatedString() + ", Total of bytes serialized " + lengthOf);

            watch.Restart();
            cadastro = Newtonsoft.Json.JsonConvert.DeserializeObject<src.protobufNet.runTimeTypeModel.Cadastro>(json);
            watch.Stop();
            ts = watch.Elapsed;
            Console.WriteLine("RunTime " + typeof(JsonSimulator).Name + " Deserialize a full payload of " + quantity +" Objects " + ts.ToFormatedString());
        }

        public void SerializarEDeserializarPayloadsDeCliente(int quantity)
        {
            Stopwatch watch = Stopwatch.StartNew();
            TimeSpan tsSerialization = new TimeSpan();
            TimeSpan tsDeSerialization = new TimeSpan();
            for (int i = 0; i < quantity; i++)
            {
                var cliente = protobufNet.runTimeTypeModel.StubsRunTimeTypeModel.CriarCliente();
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(cliente);
                watch.Stop();
                tsSerialization += watch.Elapsed;
                watch.Restart();
                cliente = Newtonsoft.Json.JsonConvert.DeserializeObject<src.protobufNet.runTimeTypeModel.Cliente>(json);
                tsDeSerialization += watch.Elapsed;
                watch.Restart();
            }
            Console.WriteLine("RunTime " + typeof(JsonSimulator).Name + " time accumulated to Serialize " + quantity + " Objects " + tsSerialization.ToFormatedString());
            Console.WriteLine("RunTime " + typeof(JsonSimulator).Name + " time accumulated to Deserialize " + tsDeSerialization.ToFormatedString());
        }
    }
}