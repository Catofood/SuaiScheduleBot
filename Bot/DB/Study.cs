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
    public string GetString()
    {
            return $@"
Id: {Id}
Day: {Day?.ToString() ?? "N/A"}
SchedulePosition: {SchedulePosition?.ToString() ?? "N/A"}
Week: {Week.ToString() ?? "N/A"}
LocationName: {Building ?? "N/A"}
Classroom: {Room ?? "N/A"}
LessonName: {Discipline ?? "N/A"}
TypeOfLesson: {Type ?? "N/A"}
GroupNames: {(Groups != null ? string.Join(", ", Groups) : "N/A")}
Teacher: {Teacher ?? "N/A"}
Department: {Department ?? "N/A"}
";
    }
}