using Microsoft.EntityFrameworkCore;

namespace Bot.DB;

public class ScheduleDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<WeeklySchedule> WeeklySchedules { get; set; }
    public DbSet<DailySchedule> DailySchedules { get; set; }
    public DbSet<ScheduledStudy> ScheduledStudies { get; set; }
    public DbSet<UnscheduledStudy> UnscheduledStudies { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        
        options.UseNpgsql("Host=localhost;Username=postgres;Password=1234;Database=suaiproject");
    }
}