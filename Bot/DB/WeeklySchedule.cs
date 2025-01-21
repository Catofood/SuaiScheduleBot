namespace Bot.DB;

public class WeeklySchedule
{
    public int Id { get; set; }
    public bool IsOdd { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }

    public List<DailySchedule> DailySchedules { get; set; }
}