using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Infrastructure.Persistence.Repositories;

public class CoursesRepository : ICoursesRepository
{
    private readonly AppDbContext _context;

    public CoursesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetCourses()
    {
        var courses = await _context.Courses
            .AsNoTracking()
            .ToListAsync();

        return courses;
    }

    public async Task<Guid> CreateCourse(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();

        return course.Id;
    }

    public async Task<Guid> UpdateCourse(Course course)
    {
        _context.Entry(course).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return course.Id;
    }

    public async Task<Guid> DeleteCourse(Guid id)
    {
        await _context.Courses
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
