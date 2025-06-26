using DevelopmentSucks.Application.Contracts.DTO;
using DevelopmentSucks.Application.Services.Identity.Auth;
using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using DevelopmentSucks.Domain.Repositories.Identity;

namespace DevelopmentSucks.Application.Services.Identity.Register;

public class AuthService : IAuthService
{
    private readonly IPasswordHasher _hasher;
    private readonly IAuthRepository _repository;
    private readonly IJwtService _jwtService;

    public AuthService(IPasswordHasher hasher, 
        IAuthRepository repository,
        IJwtService jwtService)
    {
        _hasher = hasher;
        _repository = repository;
        _jwtService = jwtService;
    }

    public async Task<Guid?> RegisterUser(RegisterDto dto)
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

    public async Task<string?> LoginUser(LoginDto dto)
    {
        var user = await _repository.LoginAsync(dto.Username, dto.Password);
        if (user == null) return null;

        return _jwtService.GenerateToken(user.Id.ToString(), dto.Username, new List<string>());
    } 
}
