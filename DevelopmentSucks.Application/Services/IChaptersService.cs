using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Application.Services;

public interface IChaptersService
{
    Task<Guid> CreateChapter(Chapter chapter);
    Task<List<Chapter>> GetAllChapters();
    Task<Chapter?> GetChapterById(Guid id);
    Task<Guid> UpdateChapter(Chapter chapter);
    Task<Guid> DeleteChapter(Guid id);
}