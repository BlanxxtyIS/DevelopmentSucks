using DevelopmentSucks.Application.Contracts.DTO;
using DevelopmentSucks.Application.Services;
using DevelopmentSucks.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevelopmentSucks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController: ControllerBase
{
    private readonly ILessonsService _lessonService;

    public LessonsController(ILessonsService lessonsService)
    {
        _lessonService = lessonsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Lesson>>> GetAllLessons()
    {
        var lessons = await _lessonService.GetAllLessons();
        return Ok(lessons); 
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateLesson([FromBody] LessonDto dto)
    {
        var lesson = new Lesson
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Order = dto.Order,
            Content = dto.Content,
            ChapterId = dto.ChapterId
        };

        var createdLessonId = await _lessonService.CreateLesson(lesson);
        return Ok(lesson);
    }

    [HttpPut]
    public async Task<ActionResult<Guid>> UpdatedLesson([FromBody] LessonDto dto)
    {
        if (dto.Id == Guid.Empty || dto.Id == null) 
            return BadRequest("Ошибка с ID");

        var lesson = new Lesson
        {
            Id = dto.Id.Value,
            Title = dto.Title,
            Order = dto.Order,
            Content = dto.Content,
            ChapterId = dto.ChapterId
        }; 

        var updatedLessonId = await _lessonService.UpdateLesson(lesson);
        return Ok(updatedLessonId);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> DeleteLesson(Guid id)
    {
        var deletedLessonId = await _lessonService.DeleteLesson(id);
        return Ok(deletedLessonId);
    }
}
