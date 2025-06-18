using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Domain.Repositories
{
    public interface ILessonsRepository
    {
        Task<Guid> CreateLesson(Lesson lesson);
        Task<List<Lesson>> GetLessons();
        Task<Lesson?> GetLesson(Guid id);
        Task<Guid> UpdateLesson(Lesson lesson);
        Task<Guid> DeleteLesson(Guid id);

    }
}