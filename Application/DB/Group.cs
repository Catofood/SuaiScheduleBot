namespace Application.Db;

public record Group
{
    public int Id;
    public List<User>? Users;
    public List<Study>? Studies;
    public string? Name;
}