namespace Application.DB.Entity;

public record User
{
    public long Id { get; set; }
    public long TelegramId { get; set; }
    public bool IsAdmin { get; set; } = false;
    
    public Group? Group { get; set; }
}
