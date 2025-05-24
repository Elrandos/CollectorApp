using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using inapp.Attribiutes;
using inapp.Enums;
using inapp.Interfaces.Repositories;
using inapp.Interfaces.Services;
using RegisterRequest = inapp.DTOs.Requests.RegisterRequest;
using LoginRequest = inapp.DTOs.Requests.LoginRequest;

namespace inapp.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizeController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public AuthorizeController(IAuthService authService, 
        ITokenService tokenService,
        IUserRepository userRepository)
    {
        _authService = authService;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = await _authService.RegisterAsync(request.Login, request.Email, request.Password);
        if (user == null)
        {
            return BadRequest(InnerCode.UserAlreadyExists);
        }
        var token = _tokenService.GenerateToken(user);
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

        var token = _tokenService.GenerateToken(user);
        return Ok(new { token });
    }

    [Authorize]
    [InnerCodeSwaggerResponse(StatusCodes.Status400BadRequest, InnerCode.BadRequest)]
    [InnerCodeSwaggerResponse(StatusCodes.Status400BadRequest, InnerCode.UserNotFound)]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return BadRequest(InnerCode.BadRequest);
        }
        var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));

        if (user == null)
            return BadRequest(InnerCode.UserNotFound);

        return Ok(new { user.Id, user.Login, user.Email});
        
    }

    //private string GenerateJwtToken(UserDto user)
    //{
    //    var claims = new[]
    //    {
    //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    //        new Claim(ClaimTypes.Name, user.Login),
    //        new Claim(ClaimTypes.Role, user.Role.ToString())
    //    };

    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //    var token = new JwtSecurityToken(
    //        _configuration["Jwt:Issuer"],
    //        _configuration["Jwt:Audience"],
    //        claims,
    //        expires: DateTime.UtcNow.AddHours(2),
    //        signingCredentials: creds
    //    );

    //    return new JwtSecurityTokenHandler().WriteToken(token);
    //}
}
