namespace Application.DB.Entity;

public class Department 
{
    public long Id { get; set; }
    
    public List<Event> Events { get; set; } = new();
    
    public string Name { get; set; } = string.Empty;
}
