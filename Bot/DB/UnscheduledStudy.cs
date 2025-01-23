namespace Bot.DB;

public class UnscheduledStudy
{
    public int Id { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; } = new Group();

    public string Type { get; set; }
    public string Discipline { get; set; }
    public string Department { get; set; }
}