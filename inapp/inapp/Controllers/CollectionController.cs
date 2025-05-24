using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using inapp.DTOs;
using inapp.DTOs.Requests;
using inapp.Interfaces.Repositories;
using inapp.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace inapp.Controllers;

[ApiController]
[Route("[controller]")]
public class CollectionController : ControllerBase
{
    private readonly ICollectionService _collectionService;
    private readonly IUserRepository _userRepository;

    public CollectionController(ICollectionService collectionService, IUserRepository userRepository)
    {
        _collectionService = collectionService;
        _userRepository = userRepository;
    }

    [Authorize]
    [HttpGet("GetCollection")]
    public async Task<IActionResult> GetCollectionAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userId, out var parsedId))
            return BadRequest("Nieprawid³owe ID u¿ytkownika w tokenie.");
        var user = await _userRepository.GetByIdAsync(parsedId);
        if (user == null)
            return NotFound("Nie znaleziono u¿ytkownika.");
        var result = await _collectionService.GetAllForCurrentUserAsync(user.Id);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("AddCollection")]
    public async Task<IActionResult> AddCollection(CollectionRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userId, out var parsedId))
            return BadRequest("Nieprawid³owe ID u¿ytkownika w tokenie.");
        var user = await _userRepository.GetByIdAsync(parsedId);
        if (user == null)
            return NotFound("Nie znaleziono u¿ytkownika.");
        var result = await _collectionService.AddCollectionForUserAsync(user.Id, request.Name, request.Image, request.Description);
        return Ok(result);
    }
}
