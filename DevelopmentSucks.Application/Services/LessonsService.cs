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

    public async Task<Guid> CreateLesson(Lesson lesson)
    {
        var createdLessonId = await _lessonsRepository.CreateLesson(lesson);
        return createdLessonId;
    }

    public async Task<Guid> UpdateLesson(Lesson lesson)
    {
        var updatedLessonId = await _lessonsRepository.UpdateLesson(lesson);
        return updatedLessonId;
    }

    public async Task<Guid> DeleteLesson(Guid id)
    {
        var deletedLessonId = await _lessonsRepository.DeleteLesson(id);
        return deletedLessonId;
    }
}
