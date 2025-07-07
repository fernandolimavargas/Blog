using BlogFecomercio.Context;
using BlogFecomercio.Models;
using BlogFecomercio.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogFecomercio.Services.Implementations
{
    public class CurtidaService : ICurtidaService
    {
        private readonly AppDbContext _context;
        public CurtidaService(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<string> CurtirOuDescurtir(int postId, int usuarioId)
        {
            {
                var post = await _context.Posts
                    .Include(p => p.Curtidas)
                    .FirstOrDefaultAsync(p => p.Id == postId);

                if (post == null)
                    return "Post não encontrado.";

                
                var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Usuarioid == usuarioId);
                if (!usuarioExiste)
                    return "Usuário não encontrado.";

                
                var curtidaExistente = post.Curtidas.FirstOrDefault(c => c.UsuarioId == usuarioId);

                if (curtidaExistente != null)
                {
                    
                    post.Curtidas.Remove(curtidaExistente);
                    await _context.SaveChangesAsync();
                    return "Descurtido com sucesso.";
                }
                else
                {
                    
                    post.Curtidas.Add(new Curtida
                    {
                        UsuarioId = usuarioId,
                        PostId = postId,
                        DataCurtida = DateTime.UtcNow
                    });
                    await _context.SaveChangesAsync();
                    return "Curtido com sucesso.";
                }
            }
        }

    }
}
