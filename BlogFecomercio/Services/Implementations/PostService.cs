using BlogFecomercio.Context;
using BlogFecomercio.Services.Interfaces;
using BlogFecomercio.Models;
using Microsoft.EntityFrameworkCore;
using BlogFecomercio.DTOs;

namespace BlogFecomercio.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;
        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostDTO>> GetAllPostsByUserAsync(int usuarioId)
        {
            return await _context.Posts
                .Where(p => p.Usuarioid == usuarioId).
                Select(p => new PostDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Body = p.Body,
                    DataHoraPostados = p.DataHoraPostados,
                    TagNome = p.TagNome,
                    Username = p.Usuario.Username,
                    TotalCurtidas = _context.Curtidas.Count(c => c.PostId == p.Id),
                    Comentarios = p.Comentarios.Select(c => new ComentarioDTO
                    {
                        Id = c.ComentarioId,
                        Conteudo = c.Texto,
                        Autor = c.Usuario.Username,
                        DataComentario = c.DataHoraComentario
                    }).ToList()

                }).ToListAsync();
        }

        public async Task<IEnumerable<PostDTO>> GetPostTo24HoursBefore(int usuarioId)
        {
            var usuariosSeguindo = await _context.Seguidores
                .Where(s => s.SeguidorId == usuarioId)
                .Select(s => s.UsuarioId)
                .ToListAsync();

            var posts = await _context.Posts
                .Where(p => usuariosSeguindo.Contains(p.Usuarioid) &&
                            p.DataHoraPostados >= DateTime.Now.AddHours(-24))
                .OrderByDescending(p => p.DataHoraPostados >= DateTime.UtcNow.AddHours(-24))
                .Select(p => new PostDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Body = p.Body,
                    DataHoraPostados = p.DataHoraPostados,
                    TagNome = p.TagNome,
                    Username = p.Usuario.Username,
                    TotalCurtidas = p.Curtidas.Count(),
                    Comentarios = p.Comentarios.Select(c => new ComentarioDTO
                    {
                        Id = c.ComentarioId,
                        Conteudo = c.Texto,
                        Autor = c.Usuario.Username,
                        DataComentario = c.DataHoraComentario
                    }).ToList()
                }).ToListAsync();

            return posts;

        }
        public async Task<IEnumerable<PostDTO>> GetTopCurtidasUltimas24Horas(int usuarioId)
        {
            var posts = await _context.Posts
                .Where(p => p.DataHoraPostados >= DateTime.Now.AddHours(-24))
                .OrderByDescending(p => p.Curtidas.Count)
                .Select(p => new PostDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Body = p.Body,
                    DataHoraPostados = p.DataHoraPostados,
                    TagNome = p.TagNome,
                    Username = p.Usuario.Username,
                    TotalCurtidas = p.Curtidas.Count(),
                    Comentarios = p.Comentarios.Select(c => new ComentarioDTO
                    {
                        Id = c.ComentarioId,
                        Conteudo = c.Texto,
                        Autor = c.Usuario.Username,
                        DataComentario = c.DataHoraComentario
                    }).ToList()
                }).ToListAsync();

            return posts;
        }

        public async Task<Post> AddPost(string title, string body, string tag, int userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var post = new Post
                {
                    Title = title,
                    Body = body,
                    DataHoraPostados = DateTime.Now,
                    TagNome = tag,
                    Usuarioid = userId
                };

                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return post;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("[ROLLBACK] Erro ao salvar post: " + ex.Message);
            }
        }

        public async Task<Post> UpdatePost(int postId, string title, string body, string tag, int userId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
            {
                throw new KeyNotFoundException($"Post com ID {postId} não encontrado.");
            }

            post.Title = title;
            post.Body = body;
            post.TagNome = tag;
            post.Usuarioid = userId;

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<List<Post>> GetAllPostsByTag(string tag)
        {
            var posts = await _context.Posts
                .Where(p => p.TagNome.Contains(tag))
                .ToListAsync();
            return posts;
        }

        public async Task<Post> DeletePost(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
            {
                throw new KeyNotFoundException($"Post com ID {postId} não encontrado.");
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<Post> GetPostById(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
            {
                throw new KeyNotFoundException($"Post com ID {postId} não encontrado.");
            }
            return post;
        }
    }
}
