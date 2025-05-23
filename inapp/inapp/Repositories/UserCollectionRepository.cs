using inapp.Data;
using inapp.Helpers;
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
            return new List<UserCollection>();
        }

        public async Task<UserCollection?> GetCollectionByIdAsync(Guid id)
        {
            return new UserCollection();
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

        public async Task<List<UserCollection>> GetAllUserCollectionsAsync(Guid userId)
        {
            var items = await _context.UserCollection
                .Where(ci => ci.UserId == userId)
                .ToListAsync();

            return items;
        }
    }
}
