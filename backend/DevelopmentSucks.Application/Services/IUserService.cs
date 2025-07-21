using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Application.Services;

public interface IUserService
{
    Task<List<User>> GetAllUsers();
    Task<User?> GetUser(Guid id);
    Task<Guid> CreateUser(User user);
    Task<bool> UpdateUser(User user);
    Task<bool> DeleteUser(Guid id);
}
