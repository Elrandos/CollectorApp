using inapp.DTOs;
using inapp.Models;

namespace inapp.Interfaces.Services
{
    public interface IAuthService
    {
        Task<UserDto?> Authenticate(string login, string password);
        Task<UserDto?> Register(string login, string email, string passwordHash);
        Task<UserDto> DeleteAccount(string login, string password);
    }
}
