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

    public async Task<Guid> CreateChapter(Chapter chapter)
    {
        await _context.Chapters.AddAsync(chapter);
        await _context.SaveChangesAsync();

        return chapter.Id;
    }

    public async Task<Guid> UpdateChapter(Chapter chapter)
    {
        _context.Entry(chapter).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return chapter.Id;
    }

    public async Task<Guid> DeleteChapter(Guid id)
    {
        await _context.Chapters
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
