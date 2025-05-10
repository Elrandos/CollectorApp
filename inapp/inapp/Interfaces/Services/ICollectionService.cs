using inapp.Models;

namespace inapp.Interfaces.Services
{
    public interface ICollectionService
    {
        Task<List<CollectionItem>> GetAllForCurrentUser();
    }
}
