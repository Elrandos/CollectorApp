using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using inapp.Attribiutes;
using inapp.DTOs;
using inapp.Enums;
using inapp.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using inapp.Services;

namespace inapp.Controllers;

[ApiController]
[Route("[controller]")]
public class CollectionController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ICollectionService _collectionService;
    private readonly IAuthService _authService;


    public CollectionController(IConfiguration configuration, ICollectionService collectionService, IAuthService authService)
    {
        _configuration = configuration;
        _collectionService = collectionService;
        _authService = authService;
    }

    [Authorize]
    [HttpGet("GetCollection")]
    public async Task<IActionResult> GetCollectionAsync()
    {
        var result = await _collectionService.GetAllForCurrentUser();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userId, out var parsedId))
            return BadRequest("Nieprawid³owe ID u¿ytkownika w tokenie.");

        var user = await _authService.GetByIdAsync(parsedId);

        if (user == null)
            return NotFound("Nie znaleziono u¿ytkownika.");

        return Ok(new
        {
            user.Id,
            user.Login,
            user.Email
        });
    }

    private string GenerateJwtToken(UserDto user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
