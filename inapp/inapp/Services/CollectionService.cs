using inapp.DTOs;
using inapp.Enums;
using inapp.Interfaces.Providers;
using inapp.Interfaces.Repositories;
using inapp.Interfaces.Services;
using inapp.Models;

namespace inapp.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGuidProvider _guidProvider;
        private readonly IImageStorageService _imageStorageService;
        private readonly IUserCollectionRepository _userCollectionRepository;
        public CollectionService(IUserRepository userRepository,
            IGuidProvider guidProvider,
            IImageStorageService imageStorageService,
            IUserCollectionRepository userCollectionRepository)
        {
            _userRepository = userRepository;
            _guidProvider = guidProvider;
            _imageStorageService = imageStorageService;
            _userCollectionRepository = userCollectionRepository;
        }

        public async Task<List<UserCollectionDto>> GetAllForCurrentUserAsync(Guid userId)
        {
            return await _userCollectionRepository.GetAllUserCollectionsAsync(userId);
        }

        public async Task<CommonResult> AddCollectionForUserAsync(Guid userId, string name, IFormFile? imageFile = null, string? description = null)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                CommonResult.Failure(InnerCode.UserNotFound, "");
            }

            string? imageUrl = null;
            if (imageFile != null)
            {
                imageUrl = await _imageStorageService.SaveImageAsync(imageFile);
            }

            var newCollection = new UserCollection
            {
                Id = _guidProvider.GenerateGuid(),
                UserId = userId,
                Name = name,
                Description = description,
                ImageUrl = imageUrl
            };

            await _userCollectionRepository.AddAsync(newCollection);

            return CommonResult.Success();
        }
    }
}
