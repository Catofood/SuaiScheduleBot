namespace ClassLibrary;

public class ScheduleItem
{
    public int Id { get; set; }

    public int? DayOfWeekNumber { get; set; }

    public int? LessonNumber { get; set; }

    public bool? IsWeekOdd { get; set; }

    public string? LocationName { get; set; }

    public string? Classroom { get; set; }

    public string? LessonName { get; set; }

    public string? TypeOfLesson { get; set; }

    public List<string>? GroupNames { get; set; }

    public string? Teacher { get; set; }

    public string? Department { get; set; }

    public string GetString()
    {
            return $@"
Id: {Id}
DayOfWeekNumber: {DayOfWeekNumber?.ToString() ?? "N/A"}
LessonNumber: {LessonNumber?.ToString() ?? "N/A"}
IsWeekOdd: {(IsWeekOdd.HasValue ? (IsWeekOdd.Value ? "Odd Week" : "Even Week") : "N/A")}
LocationName: {LocationName ?? "N/A"}
Classroom: {Classroom ?? "N/A"}
LessonName: {LessonName ?? "N/A"}
TypeOfLesson: {TypeOfLesson ?? "N/A"}
GroupNames: {(GroupNames != null ? string.Join(", ", GroupNames) : "N/A")}
Teacher: {Teacher ?? "N/A"}
Department: {Department ?? "N/A"}
";
    }
}