using Bot.Services;
using Microsoft.EntityFrameworkCore;

namespace Bot.Db;

public class ScheduleDbContext : DbContext
{
    // Для Dependency Injection:
    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options) : base(options)
    {
    }

    public DbSet<Study> Studies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
}