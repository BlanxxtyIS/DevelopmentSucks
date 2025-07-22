using DevelopmentSucks.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace DevelopmentSucks.Application.Contracts.DTO;

public class UserDto
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Имя пользователя обязательна")]
    [MaxLength(30, ErrorMessage = "Максимальная длина имени пользовалтеля 30 символов")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имя пользователя обязательна")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public UserRole? Role { get; set; }
}
