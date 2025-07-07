using BlogFecomercio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BlogFecomercio.Models;
using Microsoft.IdentityModel.Tokens;

namespace BlogFecomercio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<Usuario>> GetUsuarioByUsername(string username)
        {
            try
            {
                var usuario = await _usuarioService.GetByUserName(username);
                return Ok(usuario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{usuarioId}/seguidores")]
        public async Task<ActionResult<IEnumerable<Seguidor>>> GetSeguidores(int usuarioId)
        {
            try
            {
                var seguidores = await _usuarioService.GetAllFollowersAsync(usuarioId);
                return Ok(seguidores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{usuarioId}/seguindo")]
        public async Task<ActionResult<IEnumerable<Seguidor>>> GetSeguindo(int usuarioId)
        {
            try
            {
                var seguindo = await _usuarioService.GetWhoIsFollowingAsync(usuarioId);
                return Ok(seguindo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("seguir")]
        public async Task<ActionResult> SeguirUsuario(int usuarioId, int seguidorId)
        {
            try
            {
                var result = await _usuarioService.FollowUserAsync(usuarioId, seguidorId);
                if (result)
                {
                    return Ok("Usuário seguido com sucesso.");
                }
                else
                {
                    return BadRequest("Não foi possível seguir o usuário.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("deixar-seguir")]
        public async Task<ActionResult> DeixarDeSeguirUsuario(int usuarioId, int seguidorId)
        {
            try
            {
                var result = await _usuarioService.UnfollowUserAsync(usuarioId, seguidorId);
                if (result)
                {
                    return Ok("Usuário deixado de seguir com sucesso.");
                }
                else
                {
                    return BadRequest("Não foi possível deixar de seguir o usuário.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("criar-usuario")]
        public async Task<ActionResult<Usuario>> CriarUsuario(string username, string email)
        {
            if (username.IsNullOrEmpty())
            {
                return BadRequest("Username inválido ou vazio.");
            }
            if (email.IsNullOrEmpty() || !email.Contains("@"))
            {
                return BadRequest("Email inválido.");
            }
            try
            {
                var usuario = await _usuarioService.CreateUser(username, email);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message }); 
            }

        }

    } 
}
