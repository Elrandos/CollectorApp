using inapp.Models;

namespace inapp.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByLoginAsync(string login);
        Task<User?> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(string login, string email);
        Task AddAsync(User user);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}
