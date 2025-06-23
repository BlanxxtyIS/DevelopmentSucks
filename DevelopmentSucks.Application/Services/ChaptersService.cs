using DevelopmentSucks.Domain.Common;
using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Application.Services;

public class ChaptersService : IChaptersService
{
    private readonly IChaptersRepository _chaptersRepository;
    private readonly ILogger<ChaptersService> _logger;

    public ChaptersService(IChaptersRepository chaptersRepository,
        ILogger<ChaptersService> logger)
    {
        _chaptersRepository = chaptersRepository;
        _logger = logger;
    }

    public async Task<List<Chapter>> GetAllChapters(PaginingParameters pagining)
    {
        try
        {
            return await _chaptersRepository.GetChapters(pagining);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении глав в ChapterCervice");
            throw;
        }
    }

    public async Task<Chapter?> GetChapterById(Guid id)
    {
        try
        {
            return await _chaptersRepository.GetChapter(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении главы в ChapterService");
            throw;
        }
    }

    public async Task<Guid> CreateChapter(Chapter chapter)
    {
        try
        {
            return await _chaptersRepository.CreateChapter(chapter);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании главы в ChapterService");
            throw;
        }
    }

    public async Task<bool> UpdateChapter(Chapter chapter)
    {
        try
        {
            return await _chaptersRepository.UpdateChapter(chapter);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении главы в ChapterService");
            throw;
        }
    }

    public async Task<bool> DeleteChapter(Guid id)
    {
        try
        {
            return await _chaptersRepository.DeleteChapter(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении главы в ChapterService");
            throw;
        }
    }
}
