using DevelopmentSucks.Domain.Common;
using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Domain.Repositories;

public interface ICoursesRepository
{
    Task<Guid> CreateCourse(Course course);
    Task<List<Course>> GetCourses(PaginingParameters pagining);
    Task<Course?> GetCourse(Guid id);
    Task<bool> UpdateCourse(Course course);
    Task<bool> DeleteCourse(Guid id);
}