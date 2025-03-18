namespace Application.Storage.Entities;

public record User
{
    public long Id { get; set; }
    public long TelegramId { get; set; }
    public bool IsAdmin { get; set; } = false;
    // TODO: Запретить использовать GroupName больше определенной длины
    public string GroupName { get; set; } = string.Empty;
}
