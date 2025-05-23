using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using inapp.Attribiutes;
using inapp.DTOs;
using inapp.Enums;
using inapp.Interfaces.Services;
using RegisterRequest = inapp.DTOs.Requests.RegisterRequest;
using Microsoft.IdentityModel.Tokens;
using LoginRequest = inapp.DTOs.Requests.LoginRequest;

namespace inapp.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizeController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;

    public AuthorizeController(IConfiguration configuration,
        IAuthService authService)
    {
        _configuration = configuration;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = await _authService.RegisterAsync(request.Login, request.Email, request.Password);
        if (user == null)
        {
            return BadRequest(InnerCode.BadRequest);
        }
        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    [HttpPost("login")]
    [InnerCodeSwaggerResponse(StatusCodes.Status400BadRequest, InnerCode.BadRequest)]
    [InnerCodeSwaggerResponse(StatusCodes.Status400BadRequest, InnerCode.UserNotFound)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _authService.AuthenticateAsync(request.Login, request.Password);
        if (user == null)
            return Unauthorized(InnerCode.BadCredentials);

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    [Authorize]
    [InnerCodeSwaggerResponse(StatusCodes.Status400BadRequest, InnerCode.BadRequest)]
    [InnerCodeSwaggerResponse(StatusCodes.Status400BadRequest, InnerCode.UserNotFound)]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //var user = await _userManager.FindByIdAsync(userId);

        //if (user == null)
        //    return NotFound("Nie znaleziono u¿ytkownika.");

        //return Ok(new { user.Id, user.Email, user.FullName });
        return Ok();
    }

    private string GenerateJwtToken(UserDto user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, user.Role.ToString())
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
