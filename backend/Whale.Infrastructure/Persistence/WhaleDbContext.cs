using Microsoft.EntityFrameworkCore;
using Whale.Domain.Entities;

namespace Whale.Infrastructure.Persistence;

public class WhaleDbContext : DbContext
{
    public WhaleDbContext(DbContextOptions<WhaleDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<TeacherProfile> TeacherProfiles => Set<TeacherProfile>();
    public DbSet<Course> Courses => Set<Course>();


   protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<User>(e =>
    {
        e.HasKey(x => x.Id);
        e.HasIndex(x => x.Email).IsUnique();
        e.Property(x => x.Email).HasMaxLength(256).IsRequired();
        e.Property(x => x.FullName).HasMaxLength(200).IsRequired();

        // Phase 3: indexes for admin filtering/counting
        e.HasIndex(x => x.Status);
    });

    modelBuilder.Entity<TeacherProfile>(e =>
    {
        e.HasKey(x => x.Id);

        e.HasOne(x => x.User)
         .WithMany()
         .HasForeignKey(x => x.UserId)
         .OnDelete(DeleteBehavior.Cascade);

        e.HasIndex(x => x.UserId).IsUnique();

        // Phase 3: indexes for admin filtering/counting
        e.HasIndex(x => x.VerificationStatus);
    });

    modelBuilder.Entity<Course>(e =>
    {
        e.HasKey(x => x.Id);
        e.Property(x => x.Title).HasMaxLength(200).IsRequired();

        e.HasOne(x => x.TeacherUser)
         .WithMany()
         .HasForeignKey(x => x.TeacherUserId)
         .OnDelete(DeleteBehavior.Restrict);

        // Phase 3: indexes for admin filtering/counting
        e.HasIndex(x => x.Status);
        e.HasIndex(x => x.TeacherUserId);
    });

}

}
