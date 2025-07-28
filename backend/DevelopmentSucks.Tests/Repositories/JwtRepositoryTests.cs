using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Infrastructure.Identity.Auth;
using DevelopmentSucks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DevelopmentSucks.Tests.Repositories;

public class JwtRepositoryTests
{
    [Fact]
    public void GenerateAccessToken_Returns_Valid_JWT()
    {
        var jwtSettings = Options.Create(new JwtSettings
        {
            Key = "super_secret_key_123456789",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            DurationInMinutes = 60
        });

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "TestUser",
            Role = UserRole.Admin
        };

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        var context = new AppDbContext(options);
        var jwtRepository = new JwtRepository(jwtSettings, context);

        //Act
        var token = jwtRepository.GenerateAccessToken(user);

        //Assert
        var handler = new JwtSecurityTokenHandler();
        var parsedToken = handler.ReadJwtToken(token);

        Assert.Equal(user.Username, parsedToken.Claims.First(c => c.Type == ClaimTypes.Name).Value);
        Assert.Equal(user.Role.ToString(), parsedToken.Claims.First(c => c.Type == ClaimTypes.Role).Value);
        Assert.Equal(jwtSettings.Value.Issuer, parsedToken.Issuer);
        Assert.Equal(jwtSettings.Value.Audience, parsedToken.Audiences.First());
    }
}
