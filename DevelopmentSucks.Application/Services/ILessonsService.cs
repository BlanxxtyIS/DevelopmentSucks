using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Application.Services
{
    public interface ILessonsService
    {
        Task<Guid> CreateLesson(Lesson lesson);
        Task<Guid> DeleteLesson(Guid id);
        Task<List<Lesson>> GetAllLessons();
        Task<Guid> UpdateLesson(Lesson lesson);
    }
}