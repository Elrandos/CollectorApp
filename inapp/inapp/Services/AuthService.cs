using inapp.DTOs;
using inapp.Enums;
using inapp.Helpers;
using inapp.Interfaces.Repositories;
using inapp.Interfaces.Services;
using inapp.Models;

namespace inapp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasherHelper _passwordHasher;

        public AuthService(IUserRepository userRepository, PasswordHasherHelper passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto?> AuthenticateAsync(string login, string password)
        {
            var user = await _userRepository.GetByLoginAsync(login);
            if (user == null)
                return null;

            if (_passwordHasher.VerifyPassword(password, user.PasswordHash))
            {
                return new UserDto
                {
                    Id = user.Id,
                    Login = user.Login
                };
            }

            return null;
        }

        public async Task<UserDto?> RegisterAsync(string login, string email, string password)
        {
            if (await _userRepository.ExistsAsync(login, email))
                return null;

            var user = new User
            {
                Id = Guid.NewGuid(),
                Login = login,
                Email = email,
                PasswordHash = _passwordHasher.HashPassword(password),
                Role = Role.User 
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Login = user.Login
            };
        }

        public async Task<bool> DeleteAccountAsync(string login, string password)
        {
            var user = await _userRepository.GetByLoginAsync(login);
            if (user == null || !_passwordHasher.VerifyPassword(password, user.PasswordHash))
                return false;

            await _userRepository.DeleteAsync(user.Id);
            await _userRepository.SaveChangesAsync();

            return true;
        }
    }
}
