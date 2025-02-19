namespace Bot.Db;

public class User
{
    public int Id { get; set; }
    public long TelegramId { get; set; }
    public bool IsAdmin { get; set; } = false;
    public string? GroupId { get; set; }
    public Group? Group { get; set; }
}