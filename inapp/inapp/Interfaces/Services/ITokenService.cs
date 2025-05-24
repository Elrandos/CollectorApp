using inapp.DTOs;

namespace inapp.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(UserDto user);
    }
}
