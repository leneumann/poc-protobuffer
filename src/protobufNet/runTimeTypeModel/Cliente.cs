using System;
using System.Collections.Generic;
using ProtoBuf;

namespace src.protobufNet.runTimeTypeModel
{

public class Cliente
{
    public string PrimeroNome { get; set; }
    public string UltimoNome { get; set; }
    public DateTime DataNascimento { get; set; }
    public List<Endereco> Enderecos { get; set; }
}
public class Endereco
{
    public string Logradouro { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Pais { get; set; }
}

}