using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Application.Services;

public class LessonsService : ILessonsService
{
    private readonly ILessonsRepository _lessonsRepository;

    public LessonsService(ILessonsRepository lessonsRepository)
    {
        _lessonsRepository = lessonsRepository;
    }

    public async Task<List<Lesson>> GetAllLessons()
    {
        var lessons = await _lessonsRepository.GetLessons();
        return lessons;
    }

    public async Task<Lesson?> GetLessonById(Guid id)
    {
        return await _lessonsRepository.GetLesson(id);
    }

    public async Task<Guid> CreateLesson(Lesson lesson)
    {
        var createdLessonId = await _lessonsRepository.CreateLesson(lesson);
        return createdLessonId;
    }

    public async Task<bool> UpdateLesson(Lesson lesson)
    {
        return await _lessonsRepository.UpdateLesson(lesson);
    }

    public async Task<bool> DeleteLesson(Guid id)
    {
        return await _lessonsRepository.DeleteLesson(id);
    }
}
