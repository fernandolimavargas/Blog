using BlogFecomercio.Context;
using BlogFecomercio.DTOs;
using BlogFecomercio.Models;
using BlogFecomercio.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BlogFecomercio.Services.Implementations
{
    public class ComentarioService : IComentarioService
    {
        private readonly AppDbContext _context;
            
        public ComentarioService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ComentarioDTO> AddComentarioAsync(int postId, int usuarioId, string texto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var usuario = await _context.Usuarios.FindAsync(usuarioId);
                if (usuario == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                var comentario = new Comentario
                {
                    PostId = postId,
                    UsuarioId = usuarioId,
                    Texto = texto,
                    DataHoraComentario = DateTime.Now
                };



                _context.Comentarios.Add(comentario);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ComentarioDTO
                {
                    Id = comentario.ComentarioId,
                    Conteudo = comentario.Texto,
                    DataComentario = comentario.DataHoraComentario,
                    Autor = comentario.Usuario.Username,
                }; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] {ex.Message}");
                await transaction.RollbackAsync();
                throw new Exception("[ROLLBACK] Erro ao salvar comentário: " + ex.Message);
            }
        }


        public async Task<Comentario> UpdateComentarioAsync(int comentarioId, int usuarioId, string texto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var editarComentario = new Comentario
                {
                    ComentarioId = comentarioId,
                    UsuarioId = usuarioId,
                    Texto = texto,
                    DataHoraComentario = DateTime.Now
                };
                _context.Comentarios.Update(editarComentario);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return editarComentario;

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("[ROLLBACK] Erro ao atualizar comentário: " + ex.Message);
            }
        } 


        public async Task<Comentario> DeleteComentarioAsync(int comentarioId, int usuarioId)
        {
            try
            {
                var comentarioRemovido = await _context.Comentarios
                    .FirstOrDefaultAsync(c => c.ComentarioId == comentarioId && c.UsuarioId == usuarioId);
                _context.Comentarios.Remove(comentarioRemovido);
                await _context.SaveChangesAsync();
                return comentarioRemovido;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar comentário: " + ex.Message);
            }
        }
        public async Task<List<Comentario>> GetComentariosByPostIdAsync(int postId)
        {
            try
            {
                var comentarios = await _context.Comentarios
                    .Where(c => c.PostId == postId)
                    .ToListAsync();
                return comentarios;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar comentários: " + ex.Message);
            }

        }
    }
}
