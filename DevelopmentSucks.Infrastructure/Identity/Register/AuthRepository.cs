using DevelopmentSucks.Application.Services.Identity.Auth;
using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using DevelopmentSucks.Domain.Repositories.Identity;
using DevelopmentSucks.Infrastructure.Identity.Auth;
using DevelopmentSucks.Infrastructure.Identity.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevelopmentSucks.Infrastructure.Persistence.Repositories;

public class AuthRepository: IAuthRepository
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public AuthRepository(AppDbContext context, 
        IPasswordHasher passwordHasher,
        IJwtRepository jwtRepository)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid?> RegisterAsync(User request)
    {
        if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            return null;

        await _context.Users.AddAsync(request);
        await _context.SaveChangesAsync();

        return request.Id;
    }

    public async Task<User?> LoginAsync(string username, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null) return null;

        return _passwordHasher.Verify(user.PasswordHash, password) ? user : null;
    }
}
