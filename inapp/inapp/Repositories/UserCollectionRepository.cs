using inapp.Data;
using inapp.DTOs;
using inapp.Interfaces.Repositories;
using inapp.Models;
using Microsoft.EntityFrameworkCore;

namespace inapp.Repositories
{
    public class UserCollectionRepository : IUserCollectionRepository
    {
        private readonly CollectorDbContext _context;

        public UserCollectionRepository(CollectorDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserCollection>> GetUserCollections(Guid userId)
        {
            return await _context.UserCollection.Where(ci => ci.UserId == userId).ToListAsync();
        }

        public async Task<UserCollection?> GetCollectionByIdAsync(Guid id)
        {
            return await _context.UserCollection.Where(ci => ci.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(UserCollection collection)
        {
            await _context.UserCollection.AddAsync(collection);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var colectionToDelete = await GetCollectionByIdAsync(id);
            if (colectionToDelete == null)
            {
                throw new ArgumentException("Collection not found");
            }
            _context.UserCollection.Remove(colectionToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserCollectionDto>> GetAllUserCollectionsAsync(Guid userId)
        {
            var items = await _context.UserCollection
                .Where(ci => ci.UserId == userId)
                .Include(i => i.CollectionItems)
                .ToListAsync();

            var result = items.Select(x => new UserCollectionDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Items = x.CollectionItems.Select(ci => new CollectionItemDto
                {
                    Id = ci.Id,
                    Name = ci.Name,
                    Description = ci.Description,
                    ImageUrl = ci.ImageUrl
                }).ToList()
            }).ToList();

            return result;
        }
    }
}
