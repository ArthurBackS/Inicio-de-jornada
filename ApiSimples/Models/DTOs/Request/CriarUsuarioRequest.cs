using System.ComponentModel.DataAnnotations;

namespace ApiSimples.Models.DTOs.Request.CriarUsuarioRequest;

public class CriarUsuarioRequest
{
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public String Nome { get; set; } = "";

    [Required]
    [EmailAddress]
    public String Email { get; set; } = "";

    [Required]
    [MinLength(8)]
    public String Senha { get; set; } = "";

    [Range(0, 150)]
    public int Idade { get; set; }

    public CriarUsuarioRequest() { }

    public CriarUsuarioRequest(string Nome, String Senha, int Idade)
    {
        this.Nome = Nome;
        this.Senha = Senha;
        this.Idade = Idade;
    }
}