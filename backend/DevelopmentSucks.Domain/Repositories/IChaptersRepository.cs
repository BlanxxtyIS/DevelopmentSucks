using DevelopmentSucks.Domain.Common;
using DevelopmentSucks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Domain.Repositories;

public interface IChaptersRepository
{
    Task<List<Chapter>> GetChapters(PaginingParameters pagining);
    Task<Chapter?> GetChapter(Guid id);
    Task<Guid> CreateChapter(Chapter chapter);
    Task<bool> UpdateChapter(Chapter chapter);
    Task<bool> DeleteChapter(Guid id);
}
