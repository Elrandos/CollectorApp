using inapp.Data;
using inapp.Interfaces.Repositories;
using inapp.Models;

namespace inapp.Repositories
{
    public class CollectionItemRepository : ICollectionItemRepository
    {
        private readonly CollectorDbContext _context;

        public CollectionItemRepository(CollectorDbContext context)
        {
            _context = context;
        }

        public CollectionItem? GetCollectionItem(Guid itemId)
        {
            var item = _context.CollectionItems.FirstOrDefault(x => x.Id == itemId);
            return item;
        }


        public async Task AddAsync(CollectionItem item)
        {
            await _context.CollectionItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var itemToDelete = GetCollectionItem(id);
            if (itemToDelete == null)
            {
                throw new ArgumentException("Collection not found");
            }
            _context.CollectionItems.Remove(itemToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
