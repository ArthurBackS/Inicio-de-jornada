using Microsoft.AspNetCore.Mvc;
using ApiSimples.Models.UsuarioModels;
using ApiSimples.Services.UsuarioServices;

namespace ApiSimples.Controllers.UsuarioController;

[ApiController]
[Route("Usuarios")]
public class UsuarioController : ControllerBase
{
    UsuarioServices us_se = new();
    [HttpGet]
    public IActionResult ObterUsuarios()
    {
        return Ok(us_se.ReceberUsuarios());
    }
    [HttpGet("Usuario/{Nome}")]
    public IActionResult ObterUsuarioPorNome([FromRoute(Name = "Nome")] String Nome)
    {
        Usuario? u = us_se.ReceberUsuarioNome(Nome);
        if (u != null)
        {
            return Ok(u);
        }
        else
        {
            return NotFound();
        }
    }
    [HttpGet("Usuario/{Id:int}")]
    public IActionResult ObterUsuarioPorNome([FromRoute(Name = "Id")] int Id)
    {
        Usuario? u = us_se.ReceberUsuarioId(Id);
        if (u != null)
        {
            return Ok(u);
        }
        else
        {
            return NotFound();
        }
    }
    [HttpPost]
    public IActionResult CriarUsuario([FromBody]Usuario us)
    {
        if (!us_se.CriarUsuario(us.Nome))
        {
            return BadRequest("Nome ou senha incorretos, ou usuário com mesmo nome já existe.");
        }
        else
        {
            Usuario? u = us_se.ReceberUsuarioNome(us.Nome);
            return Created("Usuarios/Usuario/{u.Id}", u);
        }
    }
}