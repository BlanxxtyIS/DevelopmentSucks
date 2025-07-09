using DevelopmentSucks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Application.Contracts.DTO;

public class LessonDto
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Название урока обязательо")]
    [MaxLength(20, ErrorMessage = "Максимальная длина главы курса 20 символов")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "содержимое урока обязательна")]
    public string Content { get; set; } = string.Empty;

    [Required(ErrorMessage = "номер урока обязательна")]
    public int Order { get; set; }
    public Guid ChapterId { get; set; }
}
