namespace Bot.Db;

public record CalendarEvent()
{
    public int groupId;
    public string group;
    
    public string room;
    public string building;

    public string eventType;
    public string eventName;
    public DateTime eventTimeStart;
    public DateTime eventTimeEnd;
    
    public string teacherName;
    public string teacherPost;
    public string teacherDegree;
    public string teacherAcademicTitle;
}