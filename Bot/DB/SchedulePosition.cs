using System.ComponentModel;

namespace Bot.DB;

public enum SchedulePosition
{
    [Description("Im sorry")]
    Unknown = 0,
    
    [Description("9:30-11:00")]
    First = 1,
    
    [Description("11:10-12:40")]
    Second = 2,
    
    [Description("13:00-14:30")]
    Third = 3,
    
    [Description("15:00-16:30")]
    Fourth = 4,
    
    [Description("16:40-18:10")]
    Fifth = 5,
    
    [Description("18:30-20:00")]
    Sixth = 6,
    
    [Description("20:10-20:40")]
    Seventh = 7
}