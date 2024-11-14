namespace Database;

public class ScheduleService
{
    private string connectionString = File.ReadAllText("C:\\Users\\Catofood\\Desktop\\database.txt");

    public void AddStudyScheduleItem(int? dayOfWeekNumber, int? lessonNumber, bool? isWeekOdd, string? locationName,
        string? classroom,
        string? lessonName, string? typeOfLesson, string? groupName, string? teacher, string? department)
    {
        
    }
}