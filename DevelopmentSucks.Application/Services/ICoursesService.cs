using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Application.Services;

public interface ICoursesService
{
    Task<Guid> CreateCourse(Course course);
    Task<List<Course>> GetAllCourses();
    Task<Course?> GetCourseById(Guid id);
    Task<bool> UpdateCourse(Course course);
    Task<bool> DeleteCourse(Guid id);
}