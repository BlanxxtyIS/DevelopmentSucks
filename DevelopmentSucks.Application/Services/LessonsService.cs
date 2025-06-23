using DevelopmentSucks.Domain.Common;
using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Application.Services;

public class LessonsService : ILessonsService
{
    private readonly ILessonsRepository _lessonsRepository;
    private readonly ILogger<LessonsService> _logger;

    public LessonsService(ILessonsRepository lessonsRepository, 
        ILogger<LessonsService> logger)
    {
        _lessonsRepository = lessonsRepository;
        _logger = logger;
    }

    public async Task<List<Lesson>> GetAllLessons(PaginingParameters pagining)
    {
        try
        {
            return await _lessonsRepository.GetLessons(pagining);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении всех уроков в LessonsService");
            throw;
        }
    }

    public async Task<Lesson?> GetLessonById(Guid id)
    {
        try
        {
            return await _lessonsRepository.GetLesson(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении урока в LessonsService");
            throw;
        }
    }

    public async Task<Guid> CreateLesson(Lesson lesson)
    {
        try
        {
            return await _lessonsRepository.CreateLesson(lesson);
        } 
        catch(Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании урока в LessonsService");
            throw;
        }
    }

    public async Task<bool> UpdateLesson(Lesson lesson)
    {
        try
        {
            return await _lessonsRepository.UpdateLesson(lesson);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении урока в LessonsService");
            throw;
        }
    }

    public async Task<bool> DeleteLesson(Guid id)
    {
        try
        {
            return await _lessonsRepository.DeleteLesson(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении урока в LessonsService");
            throw;
        }
    }
}
