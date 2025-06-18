using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Domain.Repositories;

public interface ICoursesRepository
{
    Task<Guid> CreateCourse(Course course);
    Task<List<Course>> GetCourses();
    Task<Course?> GetCourse(Guid id);
    Task UpdateCourse(Course course);
    Task DeleteCourse(Guid id);
}