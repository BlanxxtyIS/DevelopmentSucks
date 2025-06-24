using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Domain.Repositories;

public interface IAuthRepository
{
    Task<Guid?> RegisterAsync(User user);
}
