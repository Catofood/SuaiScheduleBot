namespace Application.Models;

public class Pair
{
    public required string Room;
    public required string Building;

    public required string PairNumber;
    public required string EventTimeStart;
    public required string EventTimeEnd;
    
    public required string EventType;
    public required string EventName;
    
    public required string TeacherName;
    public required string TeacherPost;
    
    public required List<string> GroupNames;
}

