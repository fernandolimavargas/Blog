using Microsoft.AspNetCore.Mvc;
using BlogFecomercio.Services.Interfaces;
using BlogFecomercio.Models;
using Microsoft.IdentityModel.Tokens;
using BlogFecomercio.DTOs;

namespace BlogFecomercio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {

        private readonly IPostService _postService;
        private readonly ITagService _tagService;
        private readonly IComentarioService _comentarioService;
        private readonly IUsuarioService _usuarioService;
        public PostsController(IPostService postService, ITagService tagService, IUsuarioService usuarioService, IComentarioService comentarioService)
        {
            _postService = postService;
            _tagService = tagService;
            _usuarioService = usuarioService;
            _comentarioService = comentarioService;
        }

        [HttpGet("username={username}/meus-posts")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> BuscarPostPeloUsuarioId(string username)
        {
            try
            {
                var usuarioId = await _usuarioService.GetByUserName(username);

                var posts = await _postService.GetAllPostsByUserAsync(usuarioId.Usuarioid);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("tag-buscar/tag={tag}")]
        public async Task<ActionResult<List<Post>>> BuscarPostPelaTag(string tag)
        {
            try
            {
                var posts = await _postService.GetAllPostsByTag(tag);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("user={userId}/novo")]
        public async Task<ActionResult<Post>> CriarPost(int userId, string? title = null, string? body = null, string? tag = null)
        {
            try
            {
                if (!tag.IsNullOrEmpty())
                {
                    var tagExiste = await _tagService.GetByNameTagAsync(tag);
                    if (tagExiste == null)
                    {
                        var novaTag = await _tagService.CreateTagAsync(tag);
                    }
                }
                var post = await _postService.AddPost(title, body, tag, userId);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPost("user={userId}/editar/{postId}")]
        public async Task<ActionResult<PostDTO>> EditarPost(int postId, int userId, string? title = null, string? body = null, string? tag = null)
        {
            try
            {
                if (postId <= 0)
                {
                    return BadRequest("ID do post inválido.");
                }
                if (title == null)
                {
                    return BadRequest("Título não pode ser nulo.");
                }

                var post = await _postService.UpdatePost(postId, title, body, tag, userId);
                return Ok(post);
            }
            catch (Exception e)
            {
                return BadRequest(new { erro = e.Message });
            }
        }

        [HttpDelete("{postId}")]
        public async Task<ActionResult<Post>> DeletarPost(int postId)
        {
            try
            {
                if (postId <= 0)
                {
                    return BadRequest("ID do post inválido.");
                }

                var post = await _postService.DeletePost(postId);
                return Ok(post);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception e)
            {
                return BadRequest(new { erro = e.Message });
            }
        }
        [HttpGet("comentario-buscar/{postId}")]
        public async Task<ActionResult<List<ComentarioDTO>>> BuscarComentariosPorPostId(int postId)
        {
            try
            {
                var comentarios = await _comentarioService.GetComentariosByPostIdAsync(postId);
                return Ok(comentarios);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
        [HttpPost("comentario-novo-post")]
        public async Task<ActionResult<ComentarioDTO>> ComentarPost(int postId, int usuarioId, string comentario)
        {
            try { 
               var postExiste = await _postService.GetPostById(postId);
                if (postExiste == null)
                {
                    return NotFound("Post não encontrado.");
                }

                var usuarioExiste = await _usuarioService.GetById(usuarioId);
                if (usuarioExiste == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                var novoComentario = await _comentarioService.AddComentarioAsync(postId, usuarioId, comentario);
                return Ok(novoComentario);

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    erro = ex.Message,
                    inner = ex.InnerException?.Message,
                    stack = ex.StackTrace
                });
            }
        }

        [HttpPost("comentario-editar")]
        public async Task<ActionResult<Comentario>> EditarComentario(int comentarioId, int usuarioId, string comentario)
        {
            try
            {
                var editarComentario = await _comentarioService.UpdateComentarioAsync(comentarioId, usuarioId, comentario);
                return Ok(editarComentario);
            }
            catch
              (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("comentario-deletar/{CometarioId}")]
        public async Task<ActionResult<Comentario>> DeletarComentario(int comentarioId, int usuarioId)
        {
            try
            {
                var comentarioDeletado = await _comentarioService.DeleteComentarioAsync(comentarioId, usuarioId);
                return Ok(comentarioDeletado);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }



    }
}
