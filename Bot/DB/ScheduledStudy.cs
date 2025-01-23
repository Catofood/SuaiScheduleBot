namespace Bot.DB;

public class ScheduledStudy
{
    public int Id { get; set; }

    public int DailyScheduleId { get; set; }
    public DailySchedule DailySchedule { get; set; } = new DailySchedule();

    public int SchedulePosition { get; set; }
    public string Building { get; set; } = string.Empty;
    public string Room { get; set; } = string.Empty;
    public string Discipline { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Teacher { get; set; } = string.Empty;
}