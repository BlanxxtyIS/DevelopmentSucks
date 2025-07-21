using DevelopmentSucks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Domain.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetUsers();
    Task<User?> GetUser(Guid id);
    Task<Guid> CreateUser(User user);
    Task<bool> UpdateUser(User user);
    Task<bool> DeleteUser(Guid id);
}
