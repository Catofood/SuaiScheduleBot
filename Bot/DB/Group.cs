namespace Bot.DB;

public class Group
{
    public int Id {get; set;}
    
    public List<WeeklySchedule> WeeklySchedules {get; set;} = new List<WeeklySchedule>();
    
    public List<UnscheduledStudy> UnscheduledStudies {get; set;} = new List<UnscheduledStudy>();
    
    public List<User> Users {get; set;} = new List<User>();
    
    public string Name {get; set;}
}