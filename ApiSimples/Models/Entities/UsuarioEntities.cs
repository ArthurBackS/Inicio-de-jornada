namespace ApiSimples.Models.Entities.UsuarioEntities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

[Index(nameof(Email), IsUnique = true)]
public class UsuarioEntity
{
    [Key]
    public int Id {get; set;}

    [Required]
    public string Nome{get; set;}

    [Required]
    [EmailAddress]
    public string Email{get; set;}

    [Required]
    public string Senha{get; set;}

    [Range(0, 150)]
    public int Idade{get; set;} 
    public UsuarioEntity(int Id, String Nome, String Email, String Senha, int Idade)
    {
        this.Id = Id;
        this.Nome = Nome;
        this.Email = Email;
        this.Senha = Senha;
        this.Idade = Idade;
    }
}