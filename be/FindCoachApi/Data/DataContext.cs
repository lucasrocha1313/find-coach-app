using FindCoachApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace FindCoachApi.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options){}

    public DbSet<User> Users { get; set; }
    public DbSet<Coach> Coaches { get; set; }
    public DbSet<AreaExpertise> AreasExpertise { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Auth> Auths { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
                    .HasDiscriminator<string>("UserType")
                    .HasValue<User>("user")
                    .HasValue<Coach>("coach");
        
        modelBuilder.Entity<Request>().HasKey(r => new { r.Id, r.CoachId });

        modelBuilder.Entity<Auth>()
            .HasOne(a => a.User)
            .WithOne(u => u.Auth)
            .HasForeignKey<User>(u => u.AuthId);
    }
}