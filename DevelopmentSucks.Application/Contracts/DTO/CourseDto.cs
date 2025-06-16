using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Application.Contracts.DTO;

public class CourseDto
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Название курса обязательо")]
    [MaxLength(20, ErrorMessage = "Максимальная длина названия курса 20 символов")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Описание курса обязательо")]
    [MaxLength(100, ErrorMessage = "Максимальная длина описания курса 100 символов")]
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
