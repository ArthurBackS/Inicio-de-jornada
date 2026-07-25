namespace ApiSimples.Models.DTOs.Response.UsuarioResponse;

public class UsuarioResponse
{
    public String? Nome{get; set;} = "";
    public int Idade{get; set;}
    public UsuarioResponse(String? Nome, int Idade)
    {
        this.Nome = Nome;
        this.Idade = Idade;
    }
}