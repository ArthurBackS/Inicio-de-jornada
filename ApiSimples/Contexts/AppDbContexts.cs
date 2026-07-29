namespace ApiSimples.Contexts.AppDbContexts;

using ApiSimples.Models.Entities.UsuarioEntities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<UsuarioEntity> Usuarios{get; set;}
}