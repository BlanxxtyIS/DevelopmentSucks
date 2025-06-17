using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Application.Services;

public class CoursesService : ICoursesService
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly ILogger<CoursesService> _logger;

    public CoursesService(ICoursesRepository coursesRepository, ILogger<CoursesService> logger)
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

    public async Task<Guid> CreateCourse(Course course)
    {
        return await _coursesRepository.CreateCourse(course);
    }

    public async Task<Guid> UpdateCourse(Course course)
    {
        return await _coursesRepository.UpdateCourse(course);
    }

    public async Task<Guid> DeleteCourse(Guid id)
    {
        return await _coursesRepository.DeleteCourse(id);
    }
}
