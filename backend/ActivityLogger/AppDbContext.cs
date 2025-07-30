using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ActivityLogger;

public class AppDbContext: DbContext
{
    public DbSet<UserActivity> Activities { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Host=db;Database=DevelopmentSucks;Username=postgres;Password=Marl1neFullStack");
    }
}
