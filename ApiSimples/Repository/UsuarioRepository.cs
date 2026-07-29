using Microsoft.Data.Sqlite;
using ApiSimples.Models.Entities.UsuarioEntities;
using ApiSimples.Models.DTOs.Request.CriarUsuarioRequest;
using ApiSimples.Models.DTOs.Request.DeletarUsuarioRequest;
using Microsoft.EntityFrameworkCore;
using ApiSimples.Contexts.AppDbContexts;
using ApiSimples.Services.UsuarioServices;
using System.Threading.Tasks;

namespace ApiSimples.Repository.UsuarioRepository;

public class UsuarioRepository {
    private AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<bool> CriarUsuarioRepository(UsuarioEntity usuarioEntity)
    {
        _context.Usuarios.Add(usuarioEntity);
        int linhas = await _context.SaveChangesAsync();
        return linhas > 0;
    }
    public async Task<bool> DeletarUsuarioRepository(UsuarioEntity usuarioEntity)
    {
        _context.Usuarios.Remove(usuarioEntity);
        int linhas = await _context.SaveChangesAsync();
        return linhas > 0;
    }
    public async Task<UsuarioEntity?> ProcurarUsuarioEmailRepository(String Email)
    {
        var User = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
        return User;
    }
    public async Task<UsuarioEntity?> ProcurarUsuarioIdRepository(int Id)
    {
        var User = await _context.Usuarios.FindAsync(Id);
        return User;
    }
    public async Task<List<UsuarioEntity>?> ObterTodosUsuariosRepository()
    {
        return await _context.Usuarios.ToListAsync();
    }
    public async Task<bool> ExisteUsuarioEmail(String Email)
    {
        return await _context.Usuarios.AnyAsync(u => u.Email == Email);
    }
    public async Task<bool> ExisteUsuarioId(int Id)
    {
        return await _context.Usuarios.AnyAsync(u => u.Id == Id);
    }
}