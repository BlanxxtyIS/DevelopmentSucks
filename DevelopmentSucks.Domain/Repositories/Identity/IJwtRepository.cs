namespace DevelopmentSucks.Application.Services.Identity.Auth;

public interface IJwtRepository
{
    string GenerateToken(string userId, string username, IList<string> roles);
}
