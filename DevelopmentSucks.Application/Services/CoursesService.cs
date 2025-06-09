using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Application.Services;

public class CoursesService : ICoursesService
{
    private readonly ICoursesRepository _courseRepository;
    public CoursesService(ICoursesRepository coursesRepository)
    {
        _courseRepository = coursesRepository;
    }

    public async Task<List<Course>> GetAllCourses()
    {
        return await _courseRepository.GetCourses();
    }

    public async Task<Guid> CreateCourse(Course course)
    {
        return await _courseRepository.CreateCourse(course);
    }

    public async Task<Guid> UpdateCourse(Course course)
    {
        return await _courseRepository.UpdateCourse(course);
    }

    public async Task<Guid> DeleteCourse(Guid id)
    {
        return await _courseRepository.DeleteCourse(id);
    }
}
