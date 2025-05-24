using inapp.Models;

namespace inapp.Interfaces.Repositories
{
    public interface ICollectionItemRepository
    {
        Task AddAsync(CollectionItem item);
        Task DeleteAsync(Guid id);
    }
}
