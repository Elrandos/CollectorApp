using inapp.DTOs.Requests;
using inapp.Interfaces.Repositories;
using inapp.Interfaces.Services;
using inapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace inapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollectionItemController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICollectionItemService _collectionitemService;
        private readonly IUserRepository _userRepository;

        public CollectionItemController(IConfiguration configuration, ICollectionItemService collectionService, IUserRepository userRepository)
        {
            _configuration = configuration;
            _collectionitemService = collectionService;
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpPost("AddItem")]
        public async Task<IActionResult> AddCollection(CollectionItemRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userId, out var parsedId))
                return BadRequest("Nieprawidłowe ID użytkownika w tokenie.");
            var user = await _userRepository.GetByIdAsync(parsedId);
            if (user == null)
                return NotFound("Nie znaleziono użytkownika.");

            await _collectionitemService.AddItemToCollection(request.CollectionId, request.Name, request.Image, request.Description);
            return Ok();
        }

    }
}
