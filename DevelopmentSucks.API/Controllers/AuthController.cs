using DevelopmentSucks.Application.Contracts.DTO;
using DevelopmentSucks.Application.Services.Identity.Auth;
using DevelopmentSucks.Application.Services.Identity.Register;
using Microsoft.AspNetCore.Mvc;

namespace DevelopmentSucks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;
    public AuthController(IAuthService authService, IJwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterDto dto)
    {
        if (dto == null) return BadRequest();

        var userId = await _authService.RegisterUser(dto);
        
        return userId != null ? Ok(new { UserId = userId }) : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Пользователь с таким именем уже существует"
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginUser([FromBody] LoginUserRequest dto)
    {
        if (dto == null) return BadRequest();

        var token = await _authService.LoginUser(dto);

        return token != null ? Ok(token) :
            Unauthorized(new ErrorResponse
            {
                StatusCode = 401,
                Message = "Неверный логин или пароль"
            }); 
    }

    [HttpPost("refresh")]
    public async Task<ActionResult> Refresh([FromBody] RefreshRequestDto dto)
    {
        var refreshToken = await _jwtService.GetRefreshTokenAsync(dto.RefreshToken);
        if (refreshToken == null) return Unauthorized();

        await _jwtService.RevokeRefreshTokenAsync(dto.RefreshToken);

        var user = refreshToken.User;
        var roles = new List<string>();
        var accessToken = _jwtService.GenerateAccessToken(user.Id.ToString(), user.Username, roles);
        var newRefresh = await _jwtService.GenerateAndSaveRefreshTokenAsync(user);

        return Ok(new
        {
            accessToken = accessToken,
            refreshToken = refreshToken.Token
        });
    }
}
