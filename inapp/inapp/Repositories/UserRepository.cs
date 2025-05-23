using inapp.Data;
using inapp.Interfaces.Repositories;
using inapp.Models;
using Microsoft.EntityFrameworkCore;

namespace inapp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CollectorDbContext _context;

        public UserRepository(CollectorDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.User.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            return await _context.User.SingleOrDefaultAsync(u => u.Login == login);
        }

        public async Task<bool> ExistsAsync(string login, string email)
        {
            var result = await _context.User.AnyAsync(u => u.Login == login || u.Email == email);
            return result;
        }

        public async Task AddAsync(User user)
        {
            await _context.User.AddAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
