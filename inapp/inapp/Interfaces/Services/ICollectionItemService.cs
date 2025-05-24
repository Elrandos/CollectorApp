using inapp.DTOs;
using inapp.Models;

namespace inapp.Interfaces.Services
{
    public interface ICollectionItemService
    {
        Task AddItemToCollection(Guid collectionId, string name, IFormFile? imageFile = null, string? description = null);

        //Task<CommonResult> AddCollectionForUserAsync(Guid userId, string name, IFormFile? imageFile = null, string? description = null);
    }
}
