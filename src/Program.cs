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
            int quantity = 50000;
            var protobufSimulator = new ProtobufSimulator();
            protobufSimulator.SerializarEDeserializarPayloadDeCadastroClientes(quantity);
            protobufSimulator.SerializarEDeserializarPayloadsDeCliente(quantity);

            var annotationsSimulator = new AnnotationsSimulator();
            annotationsSimulator.SerializarEDeserializarPayloadDeCadastroClientes(quantity);
            annotationsSimulator.SerializarEDeserializarPayloadsDeCliente(quantity);

            var runTimeTypeModelSimulator = new RunTimeTypeModelSimulator();
            runTimeTypeModelSimulator.SerializarEDeserializarPayloadDeCadastroClientes(quantity);
            runTimeTypeModelSimulator.SerializarEDeserializarPayloadsDeCliente(quantity);

            var jsonSimulator = new JsonSimulator();
            jsonSimulator.SerializarEDeserializarPayloadDeCadastroClientes(quantity);
            jsonSimulator.SerializarEDeserializarPayloadsDeCliente(quantity);
        }
    }
}
