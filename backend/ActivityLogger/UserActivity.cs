namespace ActivityLogger;

public class UserActivity
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public string Action { get; set; } = default!;
    public DateTime Timestamp { get; set; }
}
