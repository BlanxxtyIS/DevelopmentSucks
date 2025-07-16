using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace DevelopmentSucks.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    private UserService(IUserRepository userRepository, 
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<List<User>> GetAllUsers()
    {
        try
        {
            return await _userRepository.GetUsers();
        } catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении всех пользователей в UserService");
            throw;
        }
    }

    public async Task<User?> GetUser(Guid id)
    {
        try
        {
            return await _userRepository.GetUser(id);
        } catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении пользователя в UserService");
            throw;
        }
    }

    public async Task<Guid> CreateUser(User user)
    {
        try
        {
            return await _userRepository.CreateUser(user);  
        } catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении создании урока в UserService");
            throw;
        }
    }

    public async Task<bool> UpdateUser(User user)
    {
        try
        {
            return await _userRepository.UpdateUser(user);
        } catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении урока в UserService");
            throw;
        }
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        try
        {
            return await _userRepository.DeleteUser(id);
        } catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении урока в UserService");
            throw;
        }
    }
}
