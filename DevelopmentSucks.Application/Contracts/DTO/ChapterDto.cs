using DevelopmentSucks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentSucks.Application.Contracts.DTO;

public class ChapterDto {
    public Guid? Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
    public Guid CourseId { get; set; }
}

