namespace ApiSimples.Models.DTOs.Response.UsuarioResponse;

public class UsuarioResponse
{
    public int Id{get; set;}
    public String? Nome{get; set;} = "";
    public String? Email{get; set;} = "";
    public int Idade{get; set;}
    public UsuarioResponse(int Id, String? Nome, String Email, int Idade)
    {
        this.Id = Id;
        this.Nome = Nome;
        this.Email = Email;
        this.Idade = Idade;
    }
}