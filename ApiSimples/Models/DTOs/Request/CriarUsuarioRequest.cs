namespace ApiSimples.Models.DTOs.Request.CriarUsuarioRequest;

public class Criar_Usuario_Request
{
    public String Nome{get; set;} = "";
    public String Senha{get; set;} = "";
    public int Idade{get; set;}
    public Criar_Usuario_Request(string Nome, String Senha, int Idade)
    {
        this.Nome = Nome;
        this.Senha = Senha;
        this.Idade = Idade;
    }
}