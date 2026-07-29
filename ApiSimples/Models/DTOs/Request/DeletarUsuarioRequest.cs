using System.ComponentModel.DataAnnotations;

namespace ApiSimples.Models.DTOs.Request.DeletarUsuarioRequest;

public class DeletarUsuarioRequest
{
    [Required]
    [EmailAddress]
    public String Email { get; set; } = "";

    [Required]
    [MinLength(8)]
    public String Senha { get; set; } = "";

    public DeletarUsuarioRequest() { }

    public DeletarUsuarioRequest(String Email, String Senha)
    {
        this.Email = Email;
        this.Senha = Senha;
    }
}