using System.ComponentModel.DataAnnotations;

namespace DevelopmentSucks.Application.Contracts.DTO;

public class UserDto
{
    public Guid? Id { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MaxLength(6)]
    public string Password { get; set; } = string.Empty;
}
