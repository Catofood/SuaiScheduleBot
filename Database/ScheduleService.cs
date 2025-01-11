namespace Database;

public class ScheduleService
{
    private string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=schedule_db;";

    public void AddStudyScheduleItem(int? dayOfWeekNumber, int? lessonNumber, bool? isWeekOdd, string? locationName,
        string? classroom,
        string? lessonName, string? typeOfLesson, string? groupName, string? teacher, string? department)
    {
        
    }
}