using DevelopmentSucks.Application.Contracts.DTO;
using DevelopmentSucks.Domain;
using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;

namespace DevelopmentSucks.Application.Services.Identity;

public class AuthService : IAuthService
{
    private readonly IPasswordHasher _hasher;
    private readonly IAuthRepository _repository;

    public AuthService(IPasswordHasher hasher, IAuthRepository repository)
    {
        _hasher = hasher;
        _repository = repository;
    }

    public async Task<Guid?> RegisterUser(UserDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = _hasher.Hash(dto.Password)
        };

        return await _repository.RegisterAsync(user);
    }
}
