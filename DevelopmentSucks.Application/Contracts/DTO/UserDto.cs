using System.ComponentModel.DataAnnotations;

namespace DevelopmentSucks.Application.Contracts.DTO;

public class UserDto
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MaxLength(6, ErrorMessage = "Max length is 6")]
    public string Password { get; set; } = string.Empty;
}
