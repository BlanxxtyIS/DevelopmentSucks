using DevelopmentSucks.Application.Contracts;
using DevelopmentSucks.Application.Contracts.DTO;
using DevelopmentSucks.Application.Services;
using DevelopmentSucks.Domain.Common;
using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DevelopmentSucks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICoursesService _courseService;

    public CoursesController(ICoursesService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Course>>> GetCourses([FromQuery] PaginingParameters pagining)
    {
        var courses = await _courseService.GetAllCourses(pagining);

        return courses.Any() ? Ok(courses) : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Курсы пустые"
        });
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<Course>> GetCourseById(Guid id)
    {
        var course = await _courseService.GetCourseById(id);

        return course != null ? Ok(course) : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Курса с таким ID нет"
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Guid>> CreateCourse([FromBody] CourseDto dto)
    {
        if (dto == null)
            return NotFound("Объект пустой");

        Course course = new Course
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            CreatedAt = DateTime.SpecifyKind(dto.CreatedAt, DateTimeKind.Utc)
        };

        var createdCourse = await _courseService.CreateCourse(course);
        return CreatedAtAction(nameof(GetCourseById), new { id = createdCourse }, createdCourse);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult> UpdateCourse([FromBody] CourseDto dto)
    {
        if (dto.Id == null) 
            return BadRequest("Id пустой");

        var updated = await _courseService.UpdateCourse(new Course
        {
            Id = dto.Id.Value,
            Title = dto.Title,
            Description = dto.Description,
            CreatedAt = dto.CreatedAt.ToUniversalTime()
        });

        return updated ? NoContent() : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Курс не найден для обновления"
        });
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> DeleteCourse(Guid id)
    {
        var deleted = await _courseService.DeleteCourse(id);

        return deleted ? NoContent() : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Курс не найден для удаления"
        });
    }
}
