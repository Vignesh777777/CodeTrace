using Backend.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Courses> Courses { get; set; }
    public DbSet<Opportunities> Opportunities { get; set; }
    public DbSet<CodingPlatformStats> CodingPlatformStats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User-UserProfile relationship
        modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        // User-CodingPlatformStats relationship
        modelBuilder.Entity<User>()
            .HasMany(u => u.CodingStats)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        // UserProfile-CodingPlatformStats relationship
        modelBuilder.Entity<UserProfile>()
            .HasMany(p => p.CodingStats)
            .WithOne(c => c.UserProfile)
            .HasForeignKey(c => c.UserProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
