using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevelopmentSucks.Infrastructure.Persistence.Repositories;

public class AuthRepository: IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid?> RegisterAsync(User user)
    {
        if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            return null;

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }
}
