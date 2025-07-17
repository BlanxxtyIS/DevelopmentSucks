using DevelopmentSucks.Domain.Common;
using DevelopmentSucks.Domain.Common.FilterParameters;
using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Domain.Repositories
{
    public interface ILessonsRepository
    {
        Task<List<Lesson>> GetLessons(LessonFilterParameters parameters);
        Task<Lesson?> GetLesson(Guid id);
        Task<Guid> CreateLesson(Lesson lesson);
        Task<bool> UpdateLesson(Lesson lesson);
        Task<bool> DeleteLesson(Guid id);
    }
}