using DevelopmentSucks.Application.Contracts.DTO;
using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Application.Services.Identity.Register;

public interface IAuthService
{
    Task<Guid?> RegisterUser(RegisterDto dto);
    Task<LoginUserResponse?> LoginUser(LoginUserRequest dto);
}