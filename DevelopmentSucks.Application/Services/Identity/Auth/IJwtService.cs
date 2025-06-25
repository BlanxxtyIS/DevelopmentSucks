namespace DevelopmentSucks.Application.Services.Identity.Auth;

public interface IJwtService
{
    string GenerateToken(string userId, string email, IList<string> roles);
}
