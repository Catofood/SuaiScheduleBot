namespace Bot.GuapRaspApiClient.DTO;

public record Groups()
{
    // int - group id
    // WeeklySchedule - events of this group
    public Dictionary<int, WeeklySchedule> SchedulesByGroupId;
}