namespace Bot.DB;

public class ScheduledActivity
{
    public int Id { get; set; }

    public int DailyScheduleId { get; set; }
    public DailySchedule DailySchedule { get; set; }

    public SchedulePosition SchedulePosition { get; set; }
    public string Building { get; set; }
    public string Classroom { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Teacher { get; set; }
}