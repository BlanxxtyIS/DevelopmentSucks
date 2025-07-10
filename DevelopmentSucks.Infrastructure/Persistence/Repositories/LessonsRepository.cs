using DevelopmentSucks.Domain.Common;
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

    public async Task<List<Lesson>> GetLessons(PaginingParameters pagining)
    {
        var lessons = await _context.Lessons
            .AsNoTracking()
            .OrderBy(l => l.Title)
            .Skip((pagining.PageNumber - 1) * pagining.PageSize)
            .Take(pagining.PageSize)
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

    public async Task<bool> UpdateLesson(Lesson lesson)
    {
        var updated = await _context.Lessons.FindAsync(lesson.Id);
        if (updated == null) return false;

        updated.Title = lesson.Title;
        updated.Content = lesson.Content;
        updated.Order = lesson.Order;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteLesson(Guid id)
    {
        var deleted = await _context.Lessons
            .Where(l => l.Id == id)
            .ExecuteDeleteAsync();

        return deleted > 0;
    }
}
