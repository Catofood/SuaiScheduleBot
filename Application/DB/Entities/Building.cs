namespace Application.DB.Entity;

public class Building
{
    public long Id { get; set; }
    
    public List<Room> Rooms { get; set; } = new();
    public List<Event> Events { get; set; } = new();
    
    public string Name { get; set; } = string.Empty;
}
