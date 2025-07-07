using BlogFecomercio.DTOs;
using BlogFecomercio.Models; 

using System.Collections;

namespace BlogFecomercio.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostDTO>> GetAllPostsByUserAsync(int usuarioId);
        Task<IEnumerable<PostDTO>> GetPostTo24HoursBefore(int usuarioId);
        Task<IEnumerable<PostDTO>> GetTopCurtidasUltimas24Horas(int usuarioId);
        Task<Post> AddPost(string title, string body, string tag, int userId);
        Task<Post> UpdatePost(int postId, string title, string body, string tag, int userId);
        Task<List<Post>> GetAllPostsByTag(string tag);
        Task<Post> DeletePost(int postId);
        Task<Post> GetPostById(int postId);
    }
}
