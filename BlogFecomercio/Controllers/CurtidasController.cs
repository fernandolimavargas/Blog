using BlogFecomercio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogFecomercio.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class CurtidasController : ControllerBase
    {
        private readonly ICurtidaService _curtidaService;

        public CurtidasController(ICurtidaService curtidaService)
        {
            _curtidaService = curtidaService;
        }

        [HttpPost("curtirOuDescutir")]
        public async Task<ActionResult<string>> CurtirOuDescurtir(int postId, int usuarioId)
        {
            try
            {
                var resultado = await _curtidaService.CurtirOuDescurtir(postId, usuarioId);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
