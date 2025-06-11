using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Domain.Repositories
{
    public interface ILessonsRepository
    {
        Task<Guid> CreateLesson(Lesson lesson);
        Task<Guid> DeleteLesson(Guid id);
        Task<List<Lesson>> GetLessons();
        Task<Guid> UpdateLesson(Lesson lesson);
    }
}