using DevelopmentSucks.Application.Services.Identity.Auth;
using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevelopmentSucks.Infrastructure.Identity.Auth;

public class JwtRepository : IJwtRepository
{
    private readonly JwtSettings _jwtSettings;
    private readonly AppDbContext _context;
    public JwtRepository(IOptions<JwtSettings> jwtSettings,
        AppDbContext context)
    {
        _jwtSettings = jwtSettings.Value;
        _context = context;
    }

    public string GenerateAccessToken(string userId, string username, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Name, username),
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<RefreshToken> GenerateAndSaveRefreshTokenAsync(User user)
    {
        var token = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            UserId = user.Id
        };

        await _context.RefreshTokens.AddAsync(token);
        await _context.SaveChangesAsync();

        return token;
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token && !rt.IsRevorked && rt.ExpiresAt > DateTime.UtcNow);
    }

    public async Task RevokeRefreshTokenAsync(string token)
    {
        var rt = await _context.RefreshTokens
            .FirstOrDefaultAsync(r => r.Token == token);
        if (rt != null)
        {
            rt.IsRevorked = true;
            await _context.SaveChangesAsync();
        }
    }
}
