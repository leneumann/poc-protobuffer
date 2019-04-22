using System;
using System.Diagnostics;


namespace src.protobufNet.annotations
{
    public class SimulatorAnnotations
    {
        public void SerializarEDeserializarPayloadDeCadastroClientes(int times)
        {
            var cadastro = StubsAnnotation.CriarCadastro(times);
            int lengthOf = 0;
            var watch = Stopwatch.StartNew();
            var stream = Serializer.Serialize<Cadastro>(cadastro);
            watch.Stop();
            lengthOf = stream.Length;
            Console.WriteLine("RunTime ProtoBuffer " + typeof(SimulatorAnnotations).Name + " SErialize " + watch.Elapsed.ToFormatedString() + ", Total bytes per serialization " + lengthOf);
            watch.Restart();
            cadastro = Serializer.Deserialize<Cadastro>(stream);
            watch.Stop();
            Console.WriteLine("RunTime ProtoBuffer " + typeof(SimulatorAnnotations).Name + " DEserialize " + watch.Elapsed.ToFormatedString());
        }


        public void  SerializarEDeserializarPayloadsDeCliente(int times)
        {
            Stopwatch watch = Stopwatch.StartNew();
            TimeSpan tsSerialization = new TimeSpan();
            TimeSpan tsDeSerialization = new TimeSpan();
            for (int i = 0; i < times; i++)
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
         Console.WriteLine("RunTime " + typeof(SimulatorAnnotations).Name + " time accumulated to SErialize " + times + " Objects " + tsSerialization.ToFormatedString());
            Console.WriteLine("RunTime " + typeof(SimulatorAnnotations).Name + " time accumulated to DeSErialize " + tsDeSerialization.ToFormatedString());
        }
    }
}