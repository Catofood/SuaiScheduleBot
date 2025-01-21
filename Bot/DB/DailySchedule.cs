namespace Bot.DB;

public class DailySchedule
{
    public int Id { get; set; }

    public int WeeklyScheduleId { get; set; }
    public WeeklySchedule WeeklySchedule { get; set; }

    public int DayOfWeekNumber { get; set; }
}