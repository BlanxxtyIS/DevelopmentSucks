using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Application.Services;

public class ChaptersService : IChaptersService
{
    private readonly IChaptersRepository _chaptersRepository;

    public ChaptersService(IChaptersRepository chaptersRepository)
    {
        _chaptersRepository = chaptersRepository;
    }

    public async Task<List<Chapter>> GetAllChapters()
    {
        return await _chaptersRepository.GetChapters();
    }

    public async Task<Chapter?> GetChapterById(Guid id)
    {
        return await _chaptersRepository.GetChapter(id);
    }

    public async Task<Guid> CreateChapter(Chapter chapter)
    {
        return await _chaptersRepository.CreateChapter(chapter);
    }

    public async Task<bool> UpdateChapter(Chapter chapter)
    {
        return await _chaptersRepository.UpdateChapter(chapter);
    }

    public async Task<bool> DeleteChapter(Guid id)
    {
        return await _chaptersRepository.DeleteChapter(id);
    }
}
