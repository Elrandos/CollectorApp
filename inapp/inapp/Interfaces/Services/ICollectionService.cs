using inapp.DTOs;
using inapp.Models;

namespace inapp.Interfaces.Services
{
    public interface ICollectionService
    {
        Task<List<UserCollectionDto>> GetAllForCurrentUserAsync(Guid userId);

        Task<CommonResult> AddCollectionForUserAsync(Guid userId, string name, IFormFile? imageFile = null, string? description = null);
    }
}
