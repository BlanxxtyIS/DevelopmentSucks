using DevelopmentSucks.Domain.Common.FilterParameters;
using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevelopmentSucks.Infrastructure.Persistence.Repositories;

public class LessonsRepository : ILessonsRepository
{
    private readonly AppDbContext _context;

    public LessonsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Lesson>> GetLessons(LessonFilterParameters parameters)
    {
        var query = _context.Lessons.AsQueryable();

        if (parameters.MinOrder.HasValue)
            query = query.Where(l => l.Order >= parameters.MinOrder.Value);

        if (parameters.MaxOrder.HasValue)
            query = query.Where(l => l.Order <= parameters.MaxOrder.Value);

        query = query.OrderBy(l => l.Order);

        query = query
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize);

        return await query
            .AsNoTracking()
            .ToListAsync();
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
