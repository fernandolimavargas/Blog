using BlogFecomercio.Models;

namespace BlogFecomercio.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> GetByUserName(string username);
        Task<Usuario> GetById(int usuarioId);
        Task<Usuario> CreateUser(string username, string email); 
        Task<IEnumerable<Seguidor>> GetAllFollowersAsync(int usuarioId);
        Task<IEnumerable<Seguidor>> GetWhoIsFollowingAsync(int usuarioId);
        Task<bool> FollowUserAsync(int usuarioId, int seguidorId);
        Task<bool> UnfollowUserAsync(int usuarioId, int seguidorId);
    }
}
