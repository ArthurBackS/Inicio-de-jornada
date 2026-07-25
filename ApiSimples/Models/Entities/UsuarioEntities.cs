using System;
using System.Collections.Generic;

namespace ApiSimples.Models.Entities.UsuarioEntities;

public class UsuarioEntity
{
    public int Id {get; set;} = 0;
    public string Nome{get; set;} = "";
    public string Senha{get; set;} = "";
    public int Idade{get; set;} = 0;
    public UsuarioEntity(int Id, String Nome, String Senha, int Idade)
    {
        this.Id = Id;
        this.Nome = Nome;
        this.Senha = Senha;
        this.Idade = Idade;
    }
}