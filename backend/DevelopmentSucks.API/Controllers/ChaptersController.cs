using DevelopmentSucks.Application.Contracts;
using DevelopmentSucks.Application.Contracts.DTO;
using DevelopmentSucks.Application.Services;
using DevelopmentSucks.Domain.Common;
using DevelopmentSucks.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevelopmentSucks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChaptersController: ControllerBase
{
    private readonly IChaptersService _chaptersService;

    public ChaptersController(IChaptersService chaptersService)
    {
        _chaptersService = chaptersService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Chapter>>> GetChapters([FromQuery] PaginingParameters pagining)
    {
        var chapters = await _chaptersService.GetAllChapters(pagining);

        return chapters.Any() ? Ok(chapters) : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Список глав пуст"
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Chapter>> GetChapterById(Guid id)
    {
        var chapter = await _chaptersService.GetChapterById(id);

        return chapter != null ? Ok(chapter) : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Главы с таким Id не существует"
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Guid>> CreateChapter([FromBody] ChapterDto dto)
    {
        var chapter = new Chapter
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Order = dto.Order,
            CourseId = dto.CourseId,
        };

        var id = await _chaptersService.CreateChapter(chapter);

        return CreatedAtAction(nameof(GetChapterById), new { id }, id);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult> UpdateChapter([FromBody] ChapterDto dto)
    {
        if (dto.Id == null || dto.Id == Guid.Empty) 
            return BadRequest("Пустой id");

        var updatedChapter = await _chaptersService.UpdateChapter(new Chapter
        {
            Id = dto.Id.Value,
            Title = dto.Title,
            Order = dto.Order,
            CourseId = dto.CourseId
        });

        return updatedChapter ? NoContent() : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Ошибка при обновлении главы"
        });
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> DeleteChapter(Guid id)
    {
        var deletedChapter = await _chaptersService.DeleteChapter(id);

        return deletedChapter ? NoContent() : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Ошибка при удалении главы"
        });
    }
}
