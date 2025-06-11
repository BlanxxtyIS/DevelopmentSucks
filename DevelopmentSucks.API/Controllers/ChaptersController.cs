using DevelopmentSucks.Application.Contracts.DTO;
using DevelopmentSucks.Application.Services;
using DevelopmentSucks.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevelopmentSucks.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ChaptersController: ControllerBase
{
    private readonly IChaptersService _chaptersService;

    public ChaptersController(IChaptersService chaptersService)
    {
        _chaptersService = chaptersService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Chapter>>> GetChapters()
    {
        var chapters = await _chaptersService.GetAllChapters();
        return Ok(chapters);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateChapter([FromBody] ChapterDto dto)
    {
        var chapter = new Chapter
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Order = dto.Order,
            CourseId = dto.CourseId,
        };

        var createdChapter = await _chaptersService.CreateChapter(chapter);
        return Ok(createdChapter);
    }

    [HttpPut]
    public async Task<ActionResult<Guid>> UpdateChapter([FromBody] ChapterDto dto)
    {
        if (dto.Id == null || dto.Id == Guid.Empty) return BadRequest("Пустой id");

        var chapter = new Chapter
        {
            Id = dto.Id.Value,
            Title = dto.Title,
            Order = dto.Order,
            CourseId = dto.CourseId,
        };

        var updatedChapterId = await _chaptersService.UpdateChapter(chapter);
        return Ok(updatedChapterId);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> DeleteChapter(Guid id)
    {
        var deletedChapterId = await _chaptersService.DeleteChapter(id);
        return Ok(deletedChapterId);
    }
}
