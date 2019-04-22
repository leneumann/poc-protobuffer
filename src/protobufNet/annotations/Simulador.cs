using System;
using System.Diagnostics;

namespace src.protobufNet.annotations
{
    public class SimuladorAnnotations
    {
        public void SerializarEDeserializarPayloadDeCadastroClientes(int times)
        {
            var cadastro = StubsAnnotation.CriarCadastro(times);
            int lengthOf = 0;
            var watch = Stopwatch.StartNew();
            var stream = Serializer.Serialize<Cadastro>(cadastro);
            watch.Stop();
            lengthOf = stream.Length;
            TimeSpan ts = watch.Elapsed;
            string elapsedTimeSerialize = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("RunTime Proto.net SErialize " + elapsedTimeSerialize + ", Total bytes per serialization " + lengthOf);
            watch = Stopwatch.StartNew();
            cadastro = Serializer.Deserialize<Cadastro>(stream);
            watch.Stop();
            ts = watch.Elapsed;
            string elapsedTimeDeserialize = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("RunTime Proto.net DEserialize " + elapsedTimeDeserialize);
        }


        public void  SerializarEDeserializarPayloadsDeCliente(int times)
        {

            Stopwatch watch = null;
            TimeSpan ts = new TimeSpan();
            for (int i = 0; i < times; i++)
            {
                var cliente = StubsAnnotation.CriarCliente();
                watch = Stopwatch.StartNew();
                var stream = Serializer.Serialize<Cliente>(cliente);
                watch.Stop();
                ts += watch.Elapsed;
            }
            string elapsedTimeSerialize = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("RunTime Proto.net SErialize " + elapsedTimeSerialize + ", Total bytes serializing " + times + " objects");
        }
    }
}