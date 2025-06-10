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
}
