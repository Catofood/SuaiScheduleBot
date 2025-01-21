namespace Bot.DB;

public class Group
{
    public int Id {get; set;}
    
    public List<WeeklySchedule> WeeklySchedules {get; set;}
    
    public List<UnscheduledActivity> UnscheduledActivities {get; set;}
    
    public List<User> Users {get; set;}
}