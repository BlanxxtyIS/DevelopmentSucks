using DevelopmentSucks.Domain.Entities;

namespace DevelopmentSucks.Application.Services
{
    public interface IChaptersService
    {
        Task<Guid> CreateChapter(Chapter chapter);
        Task<Guid> DeleteChapter(Guid id);
        Task<List<Chapter>> GetAllChapters();
        Task<Guid> UpdateChapter(Chapter chapter);
    }
}