using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Application.Services.Identity.Auth;

public interface IJwtService
{
    string GenerateAccessToken(string userId, string username, IList<string> roles);
    Task<RefreshToken> GenerateAndSaveRefreshTokenAsync(User user);
    Task<RefreshToken?> GetRefreshTokenAsync(string token);
    Task RevokeRefreshTokenAsync(string token);
}
