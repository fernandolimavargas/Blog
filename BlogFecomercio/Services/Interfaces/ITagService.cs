using BlogFecomercio.Models;

namespace BlogFecomercio.Services.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllTags();
        Task<Tag> GetByIdTagAsync(int id);
        Task<Tag> GetByNameTagAsync(string name);
        Task<Tag> CreateTagAsync(string name);
        Task<Tag> UpdateTagAsync(int id, Tag tag);
        Task<bool> DeleteTagAsync(int id);
    }
}
