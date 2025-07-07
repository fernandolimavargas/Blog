using BlogFecomercio.Services.Interfaces;
using BlogFecomercio.Models;
using BlogFecomercio.Context;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace BlogFecomercio.Services.Implementations
{
    public class TagService: ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            var tags = await _context.Tags.ToListAsync() ;
            if (tags == null)
            {
                throw new Exception("Tag não encontrada.");
            }
            else return tags;
        }

        public Task<Tag> GetByIdTagAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetByNameTagAsync(string name)
        {
            var tag = _context.Tags.FirstOrDefaultAsync(t => t.Name == name);
            if (tag == null)
            {
                throw new Exception("Tag não encontrada.");
            }
            else
            {
                return tag;
            }
        }

        public async Task<Tag> CreateTagAsync(string name)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("O nome da tag não pode ser vazio ou nulo.");
                }
                if (_context.Tags.Any(t => t.Name == name))
                {
                    throw new InvalidOperationException($"A tag '{name}' já existe.");
                }
                var novaTag = new Tag { Name = name };


                _context.Tags.Add(novaTag);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return novaTag;
            } catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("[ROLLBACK] Erro ao criar tag " + ex.Message);
            }
                
        }

        public Task<Tag> UpdateTagAsync(int id, Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTagAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
 
}

