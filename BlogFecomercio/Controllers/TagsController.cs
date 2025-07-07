using BlogFecomercio.Models;
using BlogFecomercio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using System.Collections;

namespace BlogFecomercio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("todas-tags")]
        public async Task<ActionResult<IEnumerable>> MostrarTodasTags()
        {
            try
            {
                var tags = await _tagService.GetAllTags();
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{nome}")]
        public async Task<ActionResult> BuscarPorNomeTag(string nome)
        {
            try
            {
                var nomeTag = await _tagService.GetByNameTagAsync(nome);
                if (nomeTag.Name == null)
                {
                    return BadRequest("Tag não encontrada ou inválida.");
                }
                else
                    return Ok(nomeTag);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("id-tag")]
        public async Task<ActionResult> BuscarTagPorID(int id)
        {
            try
            {
                var tag = _tagService.GetByIdTagAsync(id);
                if (tag == null)
                {
                    return NotFound("Tag não encontrada.");
                }
                else
                    return (Ok(tag));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("criar-tag")]
        public async Task<ActionResult<Tag>> CriarTag(string nome)
        {
            try
            {
               if(nome.IsNullOrEmpty())
                {
                    return BadRequest("Nome da tag não pode ser nulo ou vazio.");
                }

                var createdTag = await _tagService.CreateTagAsync(nome);
                return Ok(createdTag);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
