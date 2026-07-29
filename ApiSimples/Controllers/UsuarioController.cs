using Microsoft.AspNetCore.Mvc;
using ApiSimples.Models.DTOs.Request.CriarUsuarioRequest;
using ApiSimples.Models.DTOs.Request.DeletarUsuarioRequest;
using ApiSimples.Models.DTOs.Response.UsuarioResponse;
using ApiSimples.Services.UsuarioServices;

namespace ApiSimples.Controllers.UsuarioController;

[ApiController]
[Route("Usuarios")]
public class UsuarioControllers : ControllerBase
{
    UsuarioServices _services;
    public UsuarioControllers(UsuarioServices services)
    {
        _services = services;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsuarios()
    {
        return Ok(await _services.Obter_Todos_Usuarios_Service());
    }

    [HttpGet("Usuario/{Email}")]
    public async Task<IActionResult> GetUsuarios_Email([FromRoute(Name = "Email")] string Email)
    {
        UsuarioResponse? usuarioResponse = await _services.Procurar_Usuario_PorEmail_Service(Email);
        if (usuarioResponse != null)
        {
            return Ok(usuarioResponse);
        }
        return NotFound();
    }

    [HttpGet("Usuario/{Id:int}")]
    public async Task<IActionResult> GetUsuarios_Id([FromRoute(Name = "Id")] int Id)
    {
        UsuarioResponse? usuarioResponse = await _services.Procurar_Usuario_PorId_Service(Id);
        if (usuarioResponse != null)
        {
            return Ok(usuarioResponse);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Post_Cadastro_Usuario([FromBody] CriarUsuarioRequest usuario_request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            bool resultado = await _services.Cadastrar_Usuário_Service(usuario_request);
            if (resultado)
            {
                return Created($"Usuarios/Usuario/{usuario_request.Email}", usuario_request);
            }
            return BadRequest("Um ou mais campos estão inválidos.");
        } catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete_Usuario([FromBody] DeletarUsuarioRequest usuario_request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool resultado = await _services.Deletar_Usuario_Service(usuario_request);
        if (resultado)
        {
            return Ok();
        }
        else if (await _services.Procurar_Usuario_PorEmail_Service(usuario_request.Email) == null)
        {
            return NotFound();
        }
        return BadRequest();
    }
}