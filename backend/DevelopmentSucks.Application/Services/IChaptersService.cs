using DevelopmentSucks.Domain.Common;
using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Application.Services;

public interface IChaptersService
{
    Task<Guid> CreateChapter(Chapter chapter);
    Task<List<Chapter>> GetAllChapters(PaginingParameters pagining);
    Task<Chapter?> GetChapterById(Guid id);
    Task<bool> UpdateChapter(Chapter chapter);
    Task<bool> DeleteChapter(Guid id);
}