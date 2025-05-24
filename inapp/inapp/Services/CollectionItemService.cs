using inapp.DTOs;
using inapp.Interfaces.Providers;
using inapp.Interfaces.Repositories;
using inapp.Interfaces.Services;
using inapp.Models;

namespace inapp.Services
{
    public class CollectionItemService : ICollectionItemService
    {
        private readonly IGuidProvider _guidProvider;
        private readonly IImageStorageService _imageStorageService;
        private readonly ICollectionItemRepository _collectionItemRepository;
        public CollectionItemService(IGuidProvider guidProvider, IImageStorageService imageStorageService, ICollectionItemRepository collectionItemRepository)
        {
            _guidProvider = guidProvider;
            _imageStorageService = imageStorageService;
            _collectionItemRepository = collectionItemRepository;
        }

        public async Task AddItemToCollection(Guid collectionId, string name, IFormFile? imageFile = null, string? description = null)
        {
            string? imageUrl = null;
            if (imageFile != null)
            {
                imageUrl = await _imageStorageService.SaveImageAsync(imageFile);
            }

            var item = new CollectionItem()
            {
                Id = _guidProvider.GenerateGuid(),
                CollectionId = collectionId,
                Name = name,
                Description = description,
                ImageUrl = imageUrl
            };

            await _collectionItemRepository.AddAsync(item);
        }

        private List<UserCollectionDto> MapToDtoList(List<UserCollection> collections)
        {
            return collections.Select(c => new UserCollectionDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                Items = c.CollectionItems.Select(s=> new CollectionItemDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl
                }).ToList()
            }).ToList();
        }
    }
}
