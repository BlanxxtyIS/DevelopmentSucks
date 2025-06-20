using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace DevelopmentSucks.Application.Services;

public class CoursesService : ICoursesService
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly ILogger<CoursesService> _logger;

    public CoursesService(ICoursesRepository coursesRepository, 
        ILogger<CoursesService> logger)
    {
        _coursesRepository = coursesRepository;
        _logger = logger;
    }

    public async Task<List<Course>> GetAllCourses()
    {
        try
        {
            return await _coursesRepository.GetCourses();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении курсов");
            throw;
        }
    }

    public async Task<Course?> GetCourseById(Guid id)
    {
        try
        {
            return await _coursesRepository.GetCourse(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении курса");
            throw;
        }
    }

    public async Task<Guid> CreateCourse(Course course)
    {
        try
        {
            return await _coursesRepository.CreateCourse(course);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании курса");
            throw;
        }
    }

    public async Task<bool> UpdateCourse(Course course)
    {
        try
        {
            return await _coursesRepository.UpdateCourse(course);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении курса");
            throw;
        }
    }

    public async Task<bool> DeleteCourse(Guid id)
    {
        try
        {
            return await _coursesRepository.DeleteCourse(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении курса");
            throw;
        }
    }
}
