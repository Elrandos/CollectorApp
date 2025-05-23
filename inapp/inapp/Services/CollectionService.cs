using inapp.Data;
using inapp.DTOs;
using inapp.Enums;
using inapp.Interfaces.Providers;
using inapp.Interfaces.Repositories;
using inapp.Interfaces.Services;
using inapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inapp.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly CollectorDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IGuidProvider _guidProvider;
        private readonly IImageStorageService _imageStorageService;
        private readonly IUserCollectionRepository _userCollectionRepository;
        public CollectionService(CollectorDbContext context, IUserRepository userRepository, IGuidProvider guidProvider, IImageStorageService imageStorageService, IUserCollectionRepository userCollectionRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _guidProvider = guidProvider;
            _imageStorageService = imageStorageService;
            _userCollectionRepository = userCollectionRepository;
        }

        public async Task<List<UserCollectionDto>> GetAllForCurrentUserAsync(Guid userId)
        {

            var items = await _userCollectionRepository.GetAllUserCollectionsAsync(userId);
            return MapToDtoList(items);
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

        public List<UserCollectionDto> MapToDtoList(List<UserCollection> collections)
        {
            return collections.Select(c => new UserCollectionDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                Collections = c.CollectionItems.ToList()
            }).ToList();
        }
    }
}
