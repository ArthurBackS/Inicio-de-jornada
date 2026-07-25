namespace ApiSimples.Models.DTOs.Request.DeletarUsuarioRequest;

public class Deletar_Usuario_Request
{
    public String Nome{get; set;} = "";
    public String Senha{get; set;} = "";
    public Deletar_Usuario_Request(String Nome, String Senha) {
        this.Nome = Nome;
        this.Senha = Senha;
    }
}