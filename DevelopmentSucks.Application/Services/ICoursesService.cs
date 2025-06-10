using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Application.Services;

public interface ICoursesService
{
    Task<Guid> CreateCourse(Course course);
    Task<Guid> DeleteCourse(Guid id);
    Task<List<Course>> GetAllCourses();
    Task<Guid> UpdateCourse(Course course);
}