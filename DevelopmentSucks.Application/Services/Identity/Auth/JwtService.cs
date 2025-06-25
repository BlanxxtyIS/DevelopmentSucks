namespace DevelopmentSucks.Application.Services.Identity.Auth;

public class JwtService : IJwtService
{
    private readonly IJwtRepository _iJwtRepository;
    public JwtService(IJwtRepository iJwtRepository)
    {
        _iJwtRepository = iJwtRepository;
    }

    public string GenerateToken(string userId, string email, IList<string> roles)
    {
        return _iJwtRepository.GenerateToken(userId, email, roles);
    }
}
