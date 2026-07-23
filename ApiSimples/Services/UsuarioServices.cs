using ApiSimples.Models.UsuarioModels;
using ApiSimples.Repository.UsuarioRepository;

namespace ApiSimples.Services.UsuarioServices;

public class UsuarioServices
{
    UsuarioRepository us_re = new();
    public bool CriarUsuario(string nome)
    {
        if (String.IsNullOrWhiteSpace(nome))
        {
            return false;
        }
        else if (nome.Length < 3)
        {
            return false;
        }
        else if (us_re.ReceberUsuarioNome(nome) != null)
        {
            return false;
        }
        else
        {
            return us_re.CriarUsuario(nome);
        }
    }
    public Usuario? ReceberUsuarioNome(string nome)
    {
        return us_re.ReceberUsuarioNome(nome);
    }
    public Usuario? ReceberUsuarioId(int id)
    {
        return us_re.ReceberUsuarioId(id);
    }
    public List<Usuario>? ReceberUsuarios()
    {
        return us_re.Receber_Usuarios();
    }
}