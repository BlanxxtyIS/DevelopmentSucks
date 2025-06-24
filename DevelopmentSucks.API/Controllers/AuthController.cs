using DevelopmentSucks.Application.Contracts.DTO;
using DevelopmentSucks.Application.Services.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevelopmentSucks.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] UserDto dto)
    {
        if (dto == null) return BadRequest();

        await _authService.RegisterUser(dto);
        return Ok();
    }
}
