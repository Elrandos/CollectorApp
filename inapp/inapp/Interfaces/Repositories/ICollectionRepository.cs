using inapp.Models;

namespace inapp.Interfaces.Repositories
{
    public interface ICollectionRepository
    {
        Task<IEnumerable<UserCollection>> GetUserCollections(int userId);
        Task<UserCollection?> GetCollectionById(int id);
        Task AddCollection(UserCollection collection);
        Task DeleteCollection(int id);
    }
}
