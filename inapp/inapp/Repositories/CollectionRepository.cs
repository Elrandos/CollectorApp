using inapp.Data;
using inapp.Helpers;
using inapp.Interfaces.Repositories;
using inapp.Models;

namespace inapp.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly CollectorDbContext _context;
        private readonly PasswordHasherHelper _passwordHasher;

        public CollectionRepository(CollectorDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserCollection>> GetUserCollections(int userId)
        {
            return new List<UserCollection>();
        }

        public async Task<UserCollection?> GetCollectionById(int id)
        {
            return new UserCollection();
        }

        public async Task AddCollection(UserCollection collection)
        {

        }

        public async Task DeleteCollection(int id)
        {
            
        }
    }
}
