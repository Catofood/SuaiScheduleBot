using Bot.Db;
using Microsoft.EntityFrameworkCore;

public class DbService
{
    private readonly ScheduleDbContext _scheduleDbContext;

    public DbService(ScheduleDbContext scheduleDbContext)
    {
        _scheduleDbContext = scheduleDbContext;
    }

    private async Task ClearGroupsTable()
    {
        Console.WriteLine("Clearing groups...");
        _scheduleDbContext.Groups.RemoveRange(_scheduleDbContext.Groups);
    }

    private async Task SendStudiesToDb(List<Study> studies)
    {
        Console.WriteLine("Clearing studies...");
        _scheduleDbContext.Studies.RemoveRange(_scheduleDbContext.Studies);
        Console.WriteLine("Adding studies to database...");
        foreach (var scheduleItem in studies) _scheduleDbContext.Add(scheduleItem);
        await _scheduleDbContext.SaveChangesAsync();
        Console.WriteLine("Studies added to database.");
    }
}