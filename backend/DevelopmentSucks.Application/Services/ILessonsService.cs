using DevelopmentSucks.Domain.Common.FilterParameters;
using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Application.Services;

public interface ILessonsService
{
    Task<Guid> CreateLesson(Lesson lesson);
    Task<List<Lesson>> GetAllLessons(LessonFilterParameters parameters);
    Task<Lesson?> GetLessonById(Guid id);
    Task<bool> UpdateLesson(Lesson lesson);
    Task<bool> DeleteLesson(Guid id);
}