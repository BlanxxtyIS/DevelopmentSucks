namespace DevelopmentSucks.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool IsRevorked { get; set; }
    public Guid UserId { get; set; }
    
    public User? User { get; set; }
}
