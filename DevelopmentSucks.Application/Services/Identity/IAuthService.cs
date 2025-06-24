using DevelopmentSucks.Application.Contracts.DTO;

namespace DevelopmentSucks.Application.Services.Identity;

public interface IAuthService
{
    Task<Guid?> RegisterUser(UserDto dto);
}