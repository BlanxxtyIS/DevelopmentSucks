using DevelopmentSucks.Domain.Common;
using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Application.Services;

public interface ILessonsService
{
    Task<Guid> CreateLesson(Lesson lesson);
    Task<List<Lesson>> GetAllLessons(PaginingParameters pagining);
    Task<Lesson?> GetLessonById(Guid id);
    Task<bool> UpdateLesson(Lesson lesson);
    Task<bool> DeleteLesson(Guid id);
}