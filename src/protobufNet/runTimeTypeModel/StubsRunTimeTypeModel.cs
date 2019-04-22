using System;
using System.Collections.Generic;

namespace src.protobufNet.runTimeTypeModel
{
    public static class StubsRunTimeTypeModel
    {
        public static Cadastro CriarCadastro(int times)
        {
            var cadastro = new Cadastro() { clientes = new List<Cliente>() };
            for (int i = 0; i < times; i++) { cadastro.clientes.Add(CriarCliente()); }
            return cadastro;
        }

        public static Cliente CriarCliente()
        {
            var enderecos = CriarEnderecos();
            return new Cliente() { DataNascimento = DateTime.Now, PrimeroNome = "Chico", UltimoNome = "Bento", Enderecos = enderecos };
        }

        private static List<Endereco> CriarEnderecos()
        {
            var enderecos = new List<Endereco>();
            var endereco = new Endereco() { Logradouro = "Rua da Goiabeira", Bairro = "Vila Abobrinha", Cidade = "Santa Izabel", Pais = "Brasil" };
            for (int j = 0; j < 100; j++) { enderecos.Add(endereco); }
            return enderecos;
        }
    }
}