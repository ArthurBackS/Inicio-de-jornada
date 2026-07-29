using ApiSimples.Contexts.AppDbContexts;
using ApiSimples.Repository.UsuarioRepository;
using ApiSimples.Services.UsuarioServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
options.UseNpgsql(builder.Configuration.GetConnectionString("Postgre"));
}
);

// Registrar repository e services no container de DI
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<UsuarioServices>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
