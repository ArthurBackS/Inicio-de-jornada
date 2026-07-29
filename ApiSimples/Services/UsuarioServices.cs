using ApiSimples.Repository.UsuarioRepository;
using ApiSimples.Models.DTOs.Response.UsuarioResponse;
using ApiSimples.Models.DTOs.Request.CriarUsuarioRequest;
using ApiSimples.Models.DTOs.Request.DeletarUsuarioRequest;
using ApiSimples.Models.Entities.UsuarioEntities;
using System.ComponentModel.DataAnnotations;

namespace ApiSimples.Services.UsuarioServices;

public class UsuarioServices
{
    private UsuarioRepository _repository;

    public UsuarioServices(UsuarioRepository repository)
    {
        _repository = repository;
    }
    public async Task<bool> Cadastrar_Usuário_Service(CriarUsuarioRequest User) 
    {
        if (String.IsNullOrWhiteSpace(User.Nome) || User.Nome.Length > 30 || User.Nome.Length < 3) {
            return false;
        }
        else if (String.IsNullOrWhiteSpace(User.Email) || !new EmailAddressAttribute().IsValid(User.Email))
        {
            return false;
        }
        else if (User.Idade < 0 || User.Idade > 150) {
            return false;
        }
        else if (String.IsNullOrWhiteSpace(User.Senha) || User.Senha.Length < 8) {
            return false;
        }
        else if (await _repository.ExisteUsuarioEmail(User.Email)) {
            throw new Exception("Um usuário com mesmo email já existe.");
        }
        UsuarioEntity User_Entity = new UsuarioEntity(0, User.Nome, User.Email, User.Senha, User.Idade);
        return await _repository.CriarUsuarioRepository(User_Entity);
    }
    public async Task<bool> Deletar_Usuario_Service(DeletarUsuarioRequest User)
    {
        UsuarioEntity? UserEntity = await _repository.ProcurarUsuarioEmailRepository(User.Email);
        if (UserEntity == null)
        {
            return false;
        }
        else if (User.Senha != UserEntity.Senha)
        {
            return false;
        }
        else
        {
            await _repository.DeletarUsuarioRepository(UserEntity);
            return true;
        }
    }
    public async Task<UsuarioResponse?> Procurar_Usuario_PorEmail_Service(String Email) 
    {
        UsuarioEntity? UserEntity = await _repository.ProcurarUsuarioEmailRepository(Email);
        if (UserEntity == null) 
        {
            return null;
        }
        return new UsuarioResponse(UserEntity.Id, UserEntity.Nome, UserEntity.Email, UserEntity.Idade);
    }
    public async Task<UsuarioResponse?> Procurar_Usuario_PorId_Service(int Id) 
    {
       UsuarioEntity? UserEntity = await _repository.ProcurarUsuarioIdRepository(Id);
        if (UserEntity == null) 
        {
            return null;
        }
        return new UsuarioResponse(UserEntity.Id, UserEntity.Nome, UserEntity.Email, UserEntity.Idade);
    }
    public async Task<List<UsuarioResponse>?> Obter_Todos_Usuarios_Service() 
    {
        List<UsuarioResponse>? lista_response = new();
        List<UsuarioEntity>? lista_entity = await _repository.ObterTodosUsuariosRepository();
        if (lista_entity == null)
        {
            return null;
        }
        foreach (var us in lista_entity)
        {
            UsuarioResponse usuarioResponse = new UsuarioResponse(us.Id, us.Nome, us.Email, us.Idade);
            lista_response.Add(usuarioResponse);
        }
        return lista_response;
    }
}