namespace Bot.Db;

public class Group
{
    public int Id { get; set; }
    public List<User>? Users { get; set; }
    public List<Study>? Studies { get; set; }
    public string? Name { get; set; }
}