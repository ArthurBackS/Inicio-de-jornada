using ApiSimples.Repository.UsuarioRepository;
using ApiSimples.Models.DTOs.Response.UsuarioResponse;
using ApiSimples.Models.DTOs.Request.CriarUsuarioRequest;
using ApiSimples.Models.DTOs.Request.DeletarUsuarioRequest;
using ApiSimples.Models.Entities.UsuarioEntities;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace ApiSimples.Services.UsuarioServices;

public class UsuarioServices
{
    UsuarioRepository UsuarioRepository = new UsuarioRepository();
    public bool Cadastrar_Usuário_Service(Criar_Usuario_Request User) {
        if (String.IsNullOrWhiteSpace(User.Nome) || User.Nome.Length > 30 || User.Nome.Length < 3) {
            return false;
        }
        else if (User.Idade < 0 || User.Idade > 150) {
            return false;
        }
        else if (User.Senha.Length < 8) {
            return false;
        }
        else if (UsuarioRepository.Procurar_Usuario_Nome_Repository(User.Nome) != null) {
            return false;
        }
        else {
            return UsuarioRepository.Criar_Usuario_Repository(User);
        }
    }
    public bool Deletar_Usuario_Service(Deletar_Usuario_Request User)
    {
        return UsuarioRepository.Deletar_Usuario_Repository(User);
    }
    public UsuarioResponse? Procurar_Usuario_PorNome_Service(String Nome) 
    {
        UsuarioEntity? User_Entity = UsuarioRepository.Procurar_Usuario_Nome_Repository(Nome);
        
        if (User_Entity != null)
            {
            UsuarioResponse? User_Response = new UsuarioResponse(User_Entity.Nome, User_Entity.Idade);
            return User_Response;
            }
        return null;
    }
    public UsuarioResponse? Procurar_Usuario_PorId_Service(int Id) 
    {
        UsuarioEntity? User_Entity = UsuarioRepository.Procurar_Usuario_Id_Repository(Id);

        if (User_Entity != null)
            {
            UsuarioResponse? User_Response = new UsuarioResponse(User_Entity.Nome, User_Entity.Idade);
            return User_Response;
            }
        return null;
    }
    public List<UsuarioResponse>? Obter_Todos_Usuarios_Service() 
    {
        List<UsuarioResponse>? Lista_Usuarios_Response = new();

        List<UsuarioEntity>? Lista_Usuarios_Entities = new List<UsuarioEntity>();

        Lista_Usuarios_Entities = UsuarioRepository.Obter_Todos_Usuarios_Repository();
        if (Lista_Usuarios_Entities == null)
        {
            return null;
        }

        foreach (var usuario_entity in Lista_Usuarios_Entities)
        {
            UsuarioResponse u = new UsuarioResponse(usuario_entity.Nome, usuario_entity.Idade);
            Lista_Usuarios_Response.Add(u);
        }

        return Lista_Usuarios_Response;
    }
}