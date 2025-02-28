namespace Application.Db;

public record User
{
    public int Id;
    public long TelegramId;
    public bool IsAdmin = false;
    public string? GroupId;
    public Group? Group;
}