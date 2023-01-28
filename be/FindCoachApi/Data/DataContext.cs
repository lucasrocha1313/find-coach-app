using FindCoachApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace FindCoachApi.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Coach> Coaches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
                    .HasDiscriminator<string>("UserType")
                    .HasValue<User>("user")
                    .HasValue<Coach>("coach");
    }
}