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
    private readonly ICoursesRepository _coursesRepository;
    public CoursesService(ICoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    public async Task<List<Course>> GetAllCourses()
    {
        return await _coursesRepository.GetCourses();
    }

    public async Task<Guid> CreateCourse(Course course)
    {
        return await _coursesRepository.CreateCourse(course);
    }

    public async Task<Guid> UpdateCourse(Course course)
    {
        return await _coursesRepository.UpdateCourse(course);
    }

    public async Task<Guid> DeleteCourse(Guid id)
    {
        return await _coursesRepository.DeleteCourse(id);
    }
}
