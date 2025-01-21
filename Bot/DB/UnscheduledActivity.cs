namespace Bot.DB;

public class UnscheduledActivity
{
    public int Id { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }

    public string Type { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
}