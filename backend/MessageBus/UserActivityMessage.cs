namespace MessageBus;

public class UserActivityMessage
{
    public string UserId { get; set; } = default!;
    public string Action { get; set; } = default!;
    public DateTime Timestamp { get; set; }
}
