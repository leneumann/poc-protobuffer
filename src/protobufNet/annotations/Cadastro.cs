using System.Collections.Generic;
using ProtoBuf;

namespace src.protobufNet.annotations
{
    [ProtoContract()]
    public class Cadastro
    {
        [ProtoMember(1)]
        public List<Cliente> clientes { get; set; }
    }
}