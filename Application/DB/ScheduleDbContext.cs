using Application.DB.Entity;
using Microsoft.EntityFrameworkCore;
using Version = Application.DB.Entity.Version;

namespace Application.Db;

public class ScheduleDbContext : DbContext
{
    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Version> Versions { get; set; }
    public DbSet<Department> Departments { get; set; }
}
