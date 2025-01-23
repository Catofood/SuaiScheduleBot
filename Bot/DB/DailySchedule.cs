namespace Bot.DB;

public class DailySchedule
{
    public int Id { get; set; }
    
    public List<ScheduledStudy> ScheduledStudies {get; set;} = new List<ScheduledStudy>();

    public int WeeklyScheduleId { get; set; }
    public WeeklySchedule WeeklySchedule { get; set; } = new WeeklySchedule();

    public int DayOfWeek { get; set; }
}