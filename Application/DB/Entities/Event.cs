namespace Application.DB.Entity;

public class Event 
{
    public long Id { get; set; }
    public string EventName { get; set; } = string.Empty;
    public long? EventDateStart { get; set; }
    public long? EventDateEnd { get; set; }
    
    public List<Group> Groups { get; set; } = new();
    public List<Room> Rooms { get; set; } = new();
    public List<Teacher> Teachers { get; set; } = new();
    public Department Department { get; set; } = null!;
    
    public string EventType { get; set; } = string.Empty;
}
