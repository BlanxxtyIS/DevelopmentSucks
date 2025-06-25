using DevelopmentSucks.Application.Contracts.DTO;
using DevelopmentSucks.Application.Services.Identity.Auth;
using DevelopmentSucks.Application.Services.Identity.Register;
using Microsoft.AspNetCore.Mvc;

namespace DevelopmentSucks.API.Controllers;

[ApiController]
[Route("[controller]")]
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
    public async Task<ActionResult> RegisterUser([FromBody] UserDto dto)
    {
        if (dto == null) return BadRequest();

        var userId = await _authService.RegisterUser(dto);
        
        return userId != null ? Ok(new { UserId = userId }) : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Пользователь с таким именем уже существует"
        });
    }


    [HttpGet("token")]
    public async Task<ActionResult> GetToken()
    {
        var token = _jwtService.GenerateToken("123", "test@example.com", new List<string> { "Admin" });
        return Ok(new { access_token = token });
    }
}
