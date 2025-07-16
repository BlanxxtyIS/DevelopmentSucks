using DevelopmentSucks.Application.Contracts.DTO;
using DevelopmentSucks.Application.Services;
using DevelopmentSucks.Domain.Common;
using DevelopmentSucks.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<ActionResult<List<Lesson>>> GetAllLessons([FromQuery] PaginingParameters pagining)
    {
        var lessons = await _lessonService.GetAllLessons(pagining);
        return Ok(lessons); 
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Lesson>> GetLessonById(Guid id)
    {
        var lesson = await _lessonService.GetLessonById(id);

        return lesson != null ? Ok(lesson) : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Урока с таким Id нету"
        });
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
        return CreatedAtAction(nameof(GetLessonById), new { id = createdLessonId }, createdLessonId);
    }

    [HttpPut]
    public async Task<ActionResult> UpdatedLesson([FromBody] LessonDto dto)
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

        var updatedLesson = await _lessonService.UpdateLesson(lesson);
        return updatedLesson ? NoContent() : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Ошибка при обновлении урока"
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLesson(Guid id)
    {
        var deletedLessonId = await _lessonService.DeleteLesson(id);

        return deletedLessonId ? NoContent() : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Ошибка при удалении курса"
        });
    }
}
