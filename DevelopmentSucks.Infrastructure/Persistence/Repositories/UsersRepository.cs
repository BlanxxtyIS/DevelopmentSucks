using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Infrastructure.Persistence.Repositories;

public class UsersRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UsersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsers()
    {
        var users = await _context.Users
            .AsNoTracking()
            .OrderBy(u => u.Username)
            .ToListAsync();

        return users;
    }

    public async Task<User?> GetUser(Guid id)
    {
        var user = await _context.Users
            .FindAsync(id);

        return user;
    }

    public async Task<Guid> CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<bool> UpdateUser(User user)
    {
        var updated = await _context.Users.FindAsync(user.Id);
        if (updated == null) return false;

        updated.Username = user.Username;
        updated.Email = user.Email;
        updated.PasswordHash = user.PasswordHash;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        var deleted = await _context.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

        return deleted > 0;
    }
}
