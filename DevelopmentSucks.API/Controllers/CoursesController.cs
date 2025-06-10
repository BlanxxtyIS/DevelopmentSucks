using DevelopmentSucks.Application.Contracts.DTO;
using DevelopmentSucks.Application.Services;
using DevelopmentSucks.Domain.Entities;
using DevelopmentSucks.Domain.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DevelopmentSucks.API.Controllers;

[ApiController]
[Route("[controller]")]
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
        return Ok(courses);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCourse([FromBody] CourseDto dto)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            CreatedAt = DateTime.SpecifyKind(dto.CreatedAt, DateTimeKind.Utc)
        };

        var createdCourse = await _courseService.CreateCourse(course);
        return Ok(createdCourse);
    }

    [HttpPut]
    public async Task<ActionResult<Guid>> UpdateCourse([FromBody] CourseDto dto)
    {
        if (dto.Id is null) return BadRequest("Id is requred for update");

        var course = new Course
        {
            Id = dto.Id.Value,
            Title = dto.Title,
            Description = dto.Description,
            CreatedAt = DateTime.SpecifyKind(dto.CreatedAt, DateTimeKind.Utc)
        };

        var updatedCourse = await _courseService.UpdateCourse(course);
        return Ok(updatedCourse);
    }

    [HttpDelete]
    public async Task<ActionResult<Guid>> DeleteCourse([FromBody] Guid id)
    {
        var deletedCourse = await _courseService.DeleteCourse(id);
        return Ok(deletedCourse);
    }
}
