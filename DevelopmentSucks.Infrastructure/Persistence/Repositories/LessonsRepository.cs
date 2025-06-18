using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Infrastructure.Persistence.Repositories;

public class LessonsRepository : ILessonsRepository
{
    private readonly AppDbContext _context;

    public LessonsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Lesson>> GetLessons()
    {
        var lessons = await _context.Lessons
            .AsNoTracking()
            .ToListAsync();
        return lessons;
    }

    public async Task<Lesson?> GetLesson(Guid id)
    {
        var lesson = await _context.Lessons
            .FindAsync(id);

        return lesson;
    } 

    public async Task<Guid> CreateLesson(Lesson lesson)
    {
        await _context.Lessons.AddAsync(lesson);
        await _context.SaveChangesAsync();

        return lesson.Id;
    }

    public async Task<Guid> UpdateLesson(Lesson lesson)
    {
        _context.Entry(lesson).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return lesson.Id;
    }

    public async Task<Guid> DeleteLesson(Guid id)
    {
        await _context.Lessons
            .Where(l => l.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
