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

        var tokens = await _authService.LoginUser(dto);

        if (tokens == null)
        {
            return Unauthorized(new ErrorResponse
            {
                StatusCode = 401,
                Message = "Неверный логин или пароль"
            });
        }

        Response.Cookies.Append("refreshToken", tokens.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None, 
            Expires = DateTime.UtcNow.AddDays(7),
            Path = "/"
        });

        return tokens != null ? Ok(new
        {
            accessToken = tokens.AccessToken
        }) :
            Unauthorized(new ErrorResponse
            {
                StatusCode = 401,
                Message = "Неверный логин или пароль"
            }); 
    }

    [HttpPost("refresh")]
    public async Task<ActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken)) return Unauthorized();

        var token = await _jwtService.GetRefreshTokenAsync(refreshToken);
        if (token == null) return Unauthorized();

        await _jwtService.RevokeRefreshTokenAsync(refreshToken);

        var user = token.User;
        var roles = new List<string>();
        var accessToken = _jwtService.GenerateAccessToken(user.Id.ToString(), user.Username, roles);
        var newRefresh = await _jwtService.GenerateAndSaveRefreshTokenAsync(user);

        Response.Cookies.Append("refreshToken", newRefresh.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(7),
            Path = "/"
        });

        return Ok(new
        {
            accessToken = accessToken
        });
    }
}
