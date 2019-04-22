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
            int times = 100000;
            var protobufSimulator = new ProtobufSimulator();
            protobufSimulator.SerializarEDeserializarPayloadDeCadastroClientes(times);
            protobufSimulator.SerializarEDeserializarPayloadsDeCliente(times);

            var simulatorAnnotations = new SimulatorAnnotations();
            simulatorAnnotations.SerializarEDeserializarPayloadDeCadastroClientes(times);
            simulatorAnnotations.SerializarEDeserializarPayloadsDeCliente(times);

            var runTimeTypeModelSimulator = new RunTimeTypeModelSimulator();
            runTimeTypeModelSimulator.SerializarEDeserializarPayloadDeCadastroClientes(times);
        }

    }
}
