using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Domain.Repositories;

public interface ICoursesRepository
{
    Task<Guid> CreateCourse(Course course);
    Task<Guid> DeleteCourse(Guid id);
    Task<List<Course>> GetCourses();
    Task<Guid> UpdateCourse(Course course);
}