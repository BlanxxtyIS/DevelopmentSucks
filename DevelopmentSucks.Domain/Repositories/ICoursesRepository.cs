using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Domain.Repositories;

public interface ICoursesRepository
{
    Task<Guid> CreateCourse(Course course);
    Task<List<Course>> GetCourses();
    Task<Guid> DeleteCourse(Guid id);
    Task<Guid> UpdateCourse(Course course);
}