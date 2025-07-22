using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Application.Services.Identity.Auth;

public class JwtService : IJwtService
{
    private readonly IJwtRepository _iJwtRepository;
    public JwtService(IJwtRepository iJwtRepository)
    {
        _iJwtRepository = iJwtRepository;
    }

    public string GenerateAccessToken(User user)
    {
        return _iJwtRepository.GenerateAccessToken(user);
    }

    public async Task<RefreshToken> GenerateAndSaveRefreshTokenAsync(User user)
    {
        return await _iJwtRepository.GenerateAndSaveRefreshTokenAsync(user);
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
    {
        return await _iJwtRepository.GetRefreshTokenAsync(token);
    }

    public async Task RevokeRefreshTokenAsync(string token)
    {
        await _iJwtRepository.RevokeRefreshTokenAsync(token);
    }
}
