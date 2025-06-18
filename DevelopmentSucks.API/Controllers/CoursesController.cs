using DevelopmentSucks.Application.Contracts.DTO;
using DevelopmentSucks.Application.Services;
using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
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
    public async Task<ActionResult<List<Course>>> GetCourses()
    {
        var courses = await _courseService.GetAllCourses();

        return courses.Any() ? Ok(courses) : NotFound(new ErrorResponse
        {
            StatusCode = 404,
            Message = "Курсы пустые"
        });
    }

    [HttpGet("{id:guid}")]
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
    public async Task<ActionResult> UpdateCourse([FromBody] CourseDto dto)
    {
        if (dto.Id == null) 
            return BadRequest("Id пустой");

        var editing = await _courseService.GetCourseById(dto.Id.Value);
        if (editing == null)
        {
            return NotFound(new ErrorResponse
            {
                StatusCode = 404,
                Message = "Курс для обновления отсутствует"
            });
        }

        editing.Title = dto.Title;
        editing.Description = dto.Description;
        editing.CreatedAt = dto.CreatedAt;

        await _courseService.UpdateCourse(editing);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> DeleteCourse(Guid id)
    {
        var course = await _courseService.GetCourseById(id);
        if (course == null)
        {
            return NotFound(new ErrorResponse
            {
                StatusCode = 404,
                Message = "Курс для удаления отсутствует"
            });
        }

        await _courseService.DeleteCourse(id);
        return NoContent();
    }
}
