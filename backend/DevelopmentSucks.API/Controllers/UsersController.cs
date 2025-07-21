using DevelopmentSucks.Application.Contracts.DTO;
using DevelopmentSucks.Application.Services;
using DevelopmentSucks.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevelopmentSucks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    //[Authorize]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return users;
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<User>> GetUserById(Guid id)
    {
        var user = await _userService.GetUser(id);

        return user != null ? Ok(user) : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Пользователя с таким Id нету"
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Guid>> CreateUser([FromBody] UserDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = dto.PasswordHash,
        };

        var createdUserId = await _userService.CreateUser(user);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUserId }, createdUserId);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UserDto dto)
    {
        if (dto.Id == Guid.Empty || dto.Id == null)
            return BadRequest("Ошибка с ID");

        var user = new User
        {
            Id = dto.Id.Value,
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = dto.PasswordHash,
        };

        var updatedUser = await _userService.UpdateUser(user);
        return updatedUser ? NoContent() : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Ошибка при обновлении данных пользователя"
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        var deletedUserId = await _userService.DeleteUser(id);

        return deletedUserId ? NoContent() : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Ошибка при удалении курса"
        });
    } 
}
