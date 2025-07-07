using BlogFecomercio.Context;
using BlogFecomercio.Services.Interfaces;
using BlogFecomercio.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BlogFecomercio.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;
        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetByUserName(string username)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == username);

            if (usuario == null)
            {
                throw new KeyNotFoundException($"Usuário {username} não encontrado.");
            }

            return usuario;
        }   


        public async Task<Usuario> GetById(int usuarioId)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Usuarioid == usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException($"Usuário com ID {usuarioId} não encontrado.");
            }
            return usuario;
        }
        public async Task<IEnumerable<Seguidor>> GetAllFollowersAsync(int usuarioId)
        {
            var seguidores = await _context.Seguidores.Where(s => s.UsuarioId == usuarioId).ToListAsync();

            return seguidores;
        }

        public async Task<IEnumerable<Seguidor>> GetWhoIsFollowingAsync(int usuarioId)
        {
            string sql = @"select s.*, u.username from SEGUIDORES s join USUARIOS u on (u.usuarioId = s.usuarioId) where s.seguidorId = {0}"; 
            var seguindo = await _context.Seguidores
                .FromSqlRaw(sql, usuarioId)

                .ToListAsync();

            return seguindo;
        }

        public async Task<bool> FollowUserAsync(int seguidorId, int UsuarioId)
        {
            if (seguidorId == UsuarioId)
            {
                throw new ArgumentException("Um usuário não pode seguir a si mesmo.");
            }

            bool jaSegue = await _context.Seguidores
                .AnyAsync(s => s.SeguidorId == seguidorId && s.UsuarioId == UsuarioId);

            if (jaSegue)
                return false;


            var novoSeguidor = new Seguidor
            {
                SeguidorId = seguidorId,
                UsuarioId = UsuarioId,
                DataSeguimento = DateTime.Now
            };

            _context.Seguidores.Add(novoSeguidor);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> UnfollowUserAsync(int seguidorId, int UsuarioId)
        {
            var seguidor = await _context.Seguidores
                .FirstOrDefaultAsync(s => s.SeguidorId == seguidorId && s.UsuarioId == UsuarioId);

            if (seguidor == null)
            {
                return false; // Não está seguindo
            }

            _context.Seguidores.Remove(seguidor);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Usuario> CreateUser(string username, string email)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(); 
            try {
                if (string.IsNullOrWhiteSpace(username))
                    throw new ArgumentException("Usuário e email não podem estar vazios.");
                else if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("Usuário e email não podem estar vazios.");

                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Username == username);
                var emailExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == email);

                if (usuarioExistente != null)
                {
                    throw new InvalidOperationException("Usuário já existem.");
                }
                if (emailExistente != null)
                {
                    throw new InvalidOperationException("Email vinculado a um usuário.");
                }

                var newUser = new Usuario
                {
                    Username = username,
                    Email = email
                };

                _context.Usuarios.Add(newUser);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync(); 

                return newUser;
            } catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("[ROLLBACK] Erro ao criar usuário " + ex.Message);
            }
        }
    }
}
