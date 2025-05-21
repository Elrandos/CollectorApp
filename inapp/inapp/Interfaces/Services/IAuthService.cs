using inapp.DTOs;

namespace inapp.Interfaces.Services
{
    public interface IAuthService
    {
        Task<UserDto?> AuthenticateAsync(string login, string password);
        Task<UserDto?> RegisterAsync(string login, string email, string passwordHash);
        Task<bool> DeleteAccountAsync(string login, string password);
    }
}
