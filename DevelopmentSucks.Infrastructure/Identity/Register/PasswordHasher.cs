using DevelopmentSucks.Domain.Repositories.Identity;

namespace DevelopmentSucks.Infrastructure.Identity.Register;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string hash, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
