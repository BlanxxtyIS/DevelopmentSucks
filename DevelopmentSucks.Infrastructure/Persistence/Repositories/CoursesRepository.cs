﻿using DevelopmentSucks.Domain.Common;
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

    public async Task<List<Course>> GetCourses(PaginingParameters pagining)
    {
        var courses = await _context.Courses
            .AsNoTracking()
            .OrderBy(c => c.Title)
            .Skip((pagining.PageNumber - 1) * pagining.PageSize)
            .Take(pagining.PageSize)
            .ToListAsync();

        return courses;
    }

    public async Task<Course?> GetCourse(Guid id)
    {
        var course = await _context.Courses
            .FindAsync(id);

        return course;
    }

    public async Task<Guid> CreateCourse(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();

        return course.Id;
    }

    public async Task<bool> UpdateCourse(Course course)
    {
        var updated = await _context.Courses.FindAsync(course.Id);
        if (updated == null) return false;

        updated.Title = course.Title;
        updated.Description = course.Description;
        updated.CreatedAt = course.CreatedAt;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCourse(Guid id)
    {
        var deleted = await _context.Courses
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        return deleted > 0;
    }
}
