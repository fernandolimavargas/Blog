using Microsoft.AspNetCore.Mvc;
using BlogFecomercio.Services.Interfaces;
using BlogFecomercio.Models;
using Microsoft.IdentityModel.Tokens;
using BlogFecomercio.DTOs;
using System.Diagnostics.Contracts;

namespace BlogFecomercio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimelineController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ITagService _tagService;
        private readonly IComentarioService _comentarioService;
        private readonly IUsuarioService _usuarioService;
        public TimelineController(IPostService postService, ITagService tagService, IUsuarioService usuarioService)
        {
            _postService = postService;
            _tagService = tagService;
            _usuarioService = usuarioService;
        }
        [HttpGet("username={username}/posts-ultimas-24-horas/")]
        public async Task<ActionResult<IEnumerable<Post>>> BuscarPostUltimas24Horas(string username)
        {
            try
            {
                var usuarioId = await _usuarioService.GetByUserName(username);
                var posts = await _postService.GetPostTo24HoursBefore(usuarioId.Usuarioid);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("username={username}/posts-mais-curtidos-24-horas/")]
        public async Task<ActionResult<IEnumerable<PostDTO>> >BuscarPostsMaisCurtidos24Horas(string username)
        {
            try {
                var usuarioId = await _usuarioService.GetByUserName(username);
                var posts = await _postService.GetTopCurtidasUltimas24Horas(usuarioId.Usuarioid);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
