using ClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace Bot;

public class ScheduleDbContext : DbContext
{
    public ScheduleDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "schedule.db");
    }

    public DbSet<ScheduleItem> Schedule { get; set; }

    public string DbPath { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Host=localhost;Username=postgres;Password=1234;Database=suaiproject");
    }
}