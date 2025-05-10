using inapp.Data;
using inapp.DTOs;
using inapp.Helpers;
using inapp.Interfaces.Services;
using inapp.Models;
using Microsoft.EntityFrameworkCore;

namespace inapp.Services
{
    public class AuthService : IAuthService
    {
        private readonly CollectorDbContext _context;
        private readonly PasswordHasherHelper _passwordHasherHelper;

        public AuthService(CollectorDbContext context, PasswordHasherHelper passwordHasherHelperHelper)
        {
            _context = context;
            _passwordHasherHelper = passwordHasherHelperHelper;
        }

        public async Task<UserDto?> Authenticate(string login, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Login == login);
            if (user == null)
            {
                return null;
            }

            if (_passwordHasherHelper.VerifyPassword(password, user.PasswordHash))
            {
                var userDto = new UserDto()
                {
                    Id = user.Id,
                    Login = user.Login
                };
                return userDto;
            }

            return null;
        }

        public async Task<UserDto?> Register(string login, string email, string password)
        {
            bool userExists = await _context.Users.AnyAsync(u => u.Login == login || u.Email == email);
            if (userExists)
            {
                return null;
            }

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = email,
                Login = login,
                PasswordHash = _passwordHasherHelper.HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto()
            {
                Id = user.Id,
                Login = user.Login
            };
        }

        public async Task<UserDto> DeleteAccount(string login, string password)
        {
            return new UserDto();
        }
    }
}
