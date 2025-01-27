namespace Db;

public class Study
{
    public int Id { get; set; }
    public int? Day { get; set; }
    public int? SchedulePosition { get; set; }
    public int? Week { get; set; }
    public List<Group>? Groups { get; set; }
    public string? Building { get; set; }
    public string? Room { get; set; }
    public string? Discipline { get; set; }
    public string? Type { get; set; }
    public string? Teacher { get; set; }
    public string? Department { get; set; }
}