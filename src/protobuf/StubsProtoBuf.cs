using System;
using Google.Protobuf.WellKnownTypes;
using src.protobuf.generatedClasses;

namespace src.protobuf
{
    public static class StubsProtoBuf
    {
        public static Cadastro CriarClientes(int times)
        {
            Cadastro.Types.Cliente[] clientes = new Cadastro.Types.Cliente[times];
            for (int i = 0; i < times; i++) { clientes[i] = CriarCliente(); }
            var cadastro = new Cadastro { Clientes = { clientes } };
            return cadastro;
        }

        public static Cadastro.Types.Cliente CriarCliente()
        {
            var enderecos = CriarEndereco(100);
            Timestamp dataNascimento = Timestamp.FromDateTime(DateTime.UtcNow);
            return new Cadastro.Types.Cliente { PrimeroNome = "Chico", UltimoNome = "Bento", DataNascimento = dataNascimento, Enderecos = { enderecos } };
        }

        private static Cadastro.Types.Cliente.Types.Endereco[] CriarEndereco(int times)
        {
            var endereco = new Cadastro.Types.Cliente.Types.Endereco { Logradouro = "Rua da Goiabeira", Bairro = "Vila Abobrinha", Cidade = "Santa Izabel", Pais = "Brasil" };
            Cadastro.Types.Cliente.Types.Endereco[] enderecos = new Cadastro.Types.Cliente.Types.Endereco[100];
            for (int i = 0; i < 100; i++) { enderecos[i] = endereco; }
            return enderecos;
        }
    }
}