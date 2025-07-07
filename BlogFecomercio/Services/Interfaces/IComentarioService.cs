using BlogFecomercio.DTOs;
using BlogFecomercio.Models;

namespace BlogFecomercio.Services.Interfaces
{
    public interface IComentarioService
    {
        public Task<ComentarioDTO> AddComentarioAsync(int postId, int usuarioId, string texto);
        public Task<Comentario> UpdateComentarioAsync(int comentarioId, int usuarioId, string texto);
        public Task<Comentario> DeleteComentarioAsync(int comentarioId, int usuarioId);
        public Task<List<Comentario>> GetComentariosByPostIdAsync(int postId);

    }
}
