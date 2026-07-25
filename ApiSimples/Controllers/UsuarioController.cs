using Microsoft.AspNetCore.Mvc;
using ApiSimples.Models.DTOs.Request.CriarUsuarioRequest;
using ApiSimples.Models.DTOs.Request.DeletarUsuarioRequest;
using ApiSimples.Models.DTOs.Response.UsuarioResponse;
using ApiSimples.Services.UsuarioServices;

namespace ApiSimples.Controllers.UsuarioController;

[ApiController]
[Route("Usuarios")]
public class UsuarioController : ControllerBase
{
    UsuarioServices UsuarioServices = new UsuarioServices();

    [HttpGet]
    public IActionResult GetUsuarios()
    {
        return Ok(UsuarioServices.Obter_Todos_Usuarios_Service());
    }

    [HttpGet("Usuario/{Nome}")]
    public IActionResult GetUsuarios_Nome([FromRoute(Name = "Nome")] string Nome)
    {
        var us = UsuarioServices.Procurar_Usuario_PorNome_Service(Nome);
        if (us != null)
        {
            return Ok(us);
        }
        return NotFound();
    }

    [HttpGet("Usuario/{Id:int}")]
    public IActionResult GetUsuarios_Id([FromRoute(Name = "Id")] int Id)
    {
        var us = UsuarioServices.Procurar_Usuario_PorId_Service(Id);
        if (us != null)
        {
            return Ok(us);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Post_Cadastro_Usuario([FromBody] Criar_Usuario_Request usuario_request)
    {
        if (UsuarioServices.Cadastrar_Usuário_Service(usuario_request))
        {
            return Created($"/Usuarios/Usuario/{usuario_request.Nome}", usuario_request);
        }
        return BadRequest("Nome ou senha inválidos, ou usuário com mesmo nome já existe.");
    }

    [HttpDelete("Usuario")]
    public IActionResult Delete_Usuario([FromBody] Deletar_Usuario_Request usuario_request)
    {
        if (UsuarioServices.Deletar_Usuario_Service(usuario_request))
        {
            return Ok();
        }
        return BadRequest("Nome ou senha incorretos.");
    }
}