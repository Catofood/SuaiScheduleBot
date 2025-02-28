namespace Application.Db;

public record Study
{
    public int Id;
    public int? Day;
    public int? SchedulePosition;
    public int? Week;
    public List<Group>? Groups;
    public string? Building;
    public string? Room;
    public string? Discipline;
    public string? Type;
    public string? Teacher;
    public string? Department;
}