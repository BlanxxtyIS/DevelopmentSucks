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

    public async Task<Guid> CreateChapter(Chapter chapter)
    {
        return await _chaptersRepository.CreateChapter(chapter);
    }

    public async Task<Guid> UpdateChapter(Chapter chapter)
    {
        await _chaptersRepository.UpdateChapter(chapter);
        return chapter.Id;
    }

    public async Task<Guid> DeleteChapter(Guid id)
    {
        await _chaptersRepository.DeleteChapter(id);
        return id;
    }
}
