using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Infrastructure.Persistence.Repositories;

public class ChaptersRepository: IChaptersRepository
{
    private readonly AppDbContext _context;

    public ChaptersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Chapter>> GetChapters()
    {
        var chapters = await _context.Chapters
            .AsNoTracking()
            .ToListAsync();

        return chapters;
    }

    public async Task<Chapter?> GetChapter(Guid id)
    {
        var chapter = await _context.Chapters
            .FindAsync(id);

        return chapter;
    }

    public async Task<Guid> CreateChapter(Chapter chapter)
    {
        await _context.Chapters.AddAsync(chapter);
        await _context.SaveChangesAsync();

        return chapter.Id;
    }

    public async Task<bool> UpdateChapter(Chapter chapter)
    {
        var updated = await _context.Chapters.FindAsync(chapter.Id);
        if (updated == null) return false;

        updated.Title = chapter.Title;
        updated.Order = chapter.Order;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteChapter(Guid id)
    {
        var deleted = await _context.Chapters
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        return deleted > 0;
    }
}
