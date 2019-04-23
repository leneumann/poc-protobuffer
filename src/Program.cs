using src.json;
using src.protobuf;
using src.protobufNet;
using src.protobufNet.annotations;
using src.protobufNet.runTimeTypeModel;

namespace protobuf_poc
{
    class Program
    {
        static void Main(string[] args)
        {
            int times = 50000;
            var protobufSimulator = new ProtobufSimulator();
            protobufSimulator.SerializarEDeserializarPayloadDeCadastroClientes(times);
            protobufSimulator.SerializarEDeserializarPayloadsDeCliente(times);

            var annotationsSimulator = new AnnotationsSimulator();
            annotationsSimulator.SerializarEDeserializarPayloadDeCadastroClientes(times);
            annotationsSimulator.SerializarEDeserializarPayloadsDeCliente(times);

            var runTimeTypeModelSimulator = new RunTimeTypeModelSimulator();
            runTimeTypeModelSimulator.SerializarEDeserializarPayloadDeCadastroClientes(times);
            runTimeTypeModelSimulator.SerializarEDeserializarPayloadsDeCliente(times);

            var jsonSimulator = new JsonSimulator();
            jsonSimulator.SerializarEDeserializarPayloadDeCadastroClientes(times); //Passando 10000 para não ocorrer erro.
            jsonSimulator.SerializarEDeserializarPayloadsDeCliente(times);
        }
    }
}
