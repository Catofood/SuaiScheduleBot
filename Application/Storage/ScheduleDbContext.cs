using Application.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Storage;

public class ScheduleDbContext : DbContext
{
    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
