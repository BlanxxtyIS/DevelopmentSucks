namespace DevelopmentSucks.Application.Services.Identity.Auth;

public class JwtService : IJwtService
{
    private readonly IJwtRepository _iJwtRepository;
    public JwtService(IJwtRepository iJwtRepository)
    {
        _iJwtRepository = iJwtRepository;
    }

    public string GenerateToken(string userId, string username, IList<string> roles)
    {
        return _iJwtRepository.GenerateToken(userId, username, roles);
    }
}
