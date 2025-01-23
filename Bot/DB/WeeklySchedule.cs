namespace Bot.DB;

public class WeeklySchedule
{
    public int Id { get; set; }
    public int Week { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; } = new Group();

    public List<DailySchedule> DailySchedules { get; set; } = new List<DailySchedule>();
}