using inapp.Data;
using inapp.Interfaces.Providers;
using inapp.Interfaces.Repositories;
using inapp.Interfaces.Services;
using inapp.Models;
using Microsoft.EntityFrameworkCore;

namespace inapp.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly CollectorDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IGuidProvider _guidProvider;
        public CollectionService(CollectorDbContext context, IUserRepository userRepository, IGuidProvider guidProvider)
        {
            _context = context;
            _userRepository = userRepository;
            _guidProvider = guidProvider;
        }

        public async Task<List<CollectionItem>> GetAllForCurrentUserAsync(Guid userId)
        {
            var items = await _context.CollectionItems
                .Where(ci => ci.UserCollection.UserId == userId)
                .ToListAsync();

            return items;
        }

        public async Task<UserCollection> AddCollectionForUserAsync(Guid userId, string name, string? description = null, string? imageUrl = null)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var newCollection = new UserCollection
            {
                Id = _guidProvider.GenerateGuid(),
                UserId = userId,
                Name = name,
                Description = description,
                ImageUrl = imageUrl
            };

            await _context.UserCollection.AddAsync(newCollection);
            await _context.SaveChangesAsync();

            return newCollection;
        }
    }
}
