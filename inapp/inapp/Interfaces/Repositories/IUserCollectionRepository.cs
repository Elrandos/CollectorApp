using inapp.DTOs;
using inapp.Models;

namespace inapp.Interfaces.Repositories
{
    public interface IUserCollectionRepository
    {
        Task<List<UserCollectionDto>> GetAllUserCollectionsAsync(Guid userId);
        Task<UserCollection?> GetCollectionByIdAsync(Guid id);
        Task AddAsync(UserCollection collection);
        Task DeleteAsync(Guid id);
    }
}
