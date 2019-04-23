using System;
using System.Diagnostics;

namespace src.protobufNet.annotations
{
    public class AnnotationsSimulator
    {
        public void SerializarEDeserializarPayloadDeCadastroClientes(int quantity)
        {
            var cadastro = StubsAnnotation.CriarCadastro(quantity);
            int lengthOf = 0;
            var watch = Stopwatch.StartNew();
            var stream = Serializer.Serialize<Cadastro>(cadastro);
            watch.Stop();
            lengthOf = stream.Length;
            Console.WriteLine("RunTime " + typeof(AnnotationsSimulator).Name + " Serialize a full payload of " + quantity + " Objects " + watch.Elapsed.ToFormatedString() + ", Total of bytes serialized " + lengthOf);
            watch.Restart();
            cadastro = Serializer.Deserialize<Cadastro>(stream);
            watch.Stop();
            Console.WriteLine("RunTime " + typeof(AnnotationsSimulator).Name + " Deserialize a full payload of " + quantity + " Objects " + watch.Elapsed.ToFormatedString());
        }

        public void SerializarEDeserializarPayloadsDeCliente(int quantity)
        {
            Stopwatch watch = Stopwatch.StartNew();
            TimeSpan tsSerialization = new TimeSpan();
            TimeSpan tsDeSerialization = new TimeSpan();
            for (int i = 0; i < quantity; i++)
            {
                var cliente = StubsAnnotation.CriarCliente();
                var stream = Serializer.Serialize<Cliente>(cliente);
                watch.Stop();
                tsSerialization += watch.Elapsed;
                watch.Restart();
                cliente = Serializer.Deserialize<Cliente>(stream);
                tsDeSerialization += watch.Elapsed;
                watch.Restart();
            }
            Console.WriteLine("RunTime " + typeof(AnnotationsSimulator).Name + " time accumulated to Serialize " + quantity + " Objects " + tsSerialization.ToFormatedString());
            Console.WriteLine("RunTime " + typeof(AnnotationsSimulator).Name + " time accumulated to Deserialize " + tsDeSerialization.ToFormatedString());
        }
    }
}