namespace ClassLibrary;

public class StudyScheduled : StudyScheduledDb
{
    public string GetText()
    {
        return $"{GetStartTime()} ({LessonNumber?.ToString() ?? "N/A"} пара)\n" +
               $"{TypeOfLesson} - {LessonName}\n" +
               $"{LocationName}, {Classroom}\n";
    }

    public string? GetFullTime()
    {
        string? time = LessonNumber switch
        {
            1 => "9:30-11:00",
            2 => "11:10-12:40",
            3 => "13:00-14:30",
            4 => "15:00-16:30",
            5 => "16:40-18:10",
            6 => "18:30-20:00",
            7 => "20:10-20:40",
            null => null,
            _ => throw new ArgumentOutOfRangeException()
        };
        return time;
    }
    public string? GetStartTime()
    {
        string? time = LessonNumber switch
        {
            1 => "9:30",
            2 => "11:10",
            3 => "13:00",
            4 => "15:00",
            5 => "16:40",
            6 => "18:30",
            7 => "20:10",
            _ => null,
        };
        return time;
    }
    public string? GetDayOfWeek()
    {
        string? dayOfWeek = DayOfWeekNumber switch
        {
            1 => "Понедельник",
            2 => "Вторник",
            3 => "Среда",
            4 => "Четверг",
            5 => "Пятница",
            6 => "Суббота",
            7 => "Воскресенье",
            _ => null,
        };
        return dayOfWeek;
    }
}