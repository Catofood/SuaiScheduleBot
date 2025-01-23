namespace Bot.DB;

public class User
{
    public int Id { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; } = new Group();

    public int TelegramId { get; set; }
    public bool IsAdmin { get; set; }
}