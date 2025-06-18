using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Application.Services
{
    public interface ILessonsService
    {
        Task<Guid> CreateLesson(Lesson lesson);
        Task<List<Lesson>> GetAllLessons();
        Task<Lesson?> GetLessonById(Guid id);
        Task<Guid> UpdateLesson(Lesson lesson);
        Task<Guid> DeleteLesson(Guid id);
    }
}