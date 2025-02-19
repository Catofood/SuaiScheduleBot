using Bot.Services;
using Microsoft.EntityFrameworkCore;

namespace Db;

public class ScheduleDbContext : DbContext
{
    public DbSet<Study> Studies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            .UseNpgsql("Host=localhost;Username=postgres;Password=1234;Database=suaiproject");
    }
}