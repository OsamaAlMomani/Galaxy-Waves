using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Net.Sockets;
using Whale.Domain.Entities;

namespace Whale.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(WhaleDbContext db)
    {
        // Ensure DB schema is applied (with retry for Docker Compose startup)
        const int maxAttempts = 15;
        var delay = TimeSpan.FromSeconds(1);

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                await db.Database.MigrateAsync();
                break;
            }
            catch (Exception ex) when (attempt < maxAttempts && IsTransientDbStartupException(ex))
            {
                await Task.Delay(delay);
                delay = TimeSpan.FromSeconds(Math.Min(delay.TotalSeconds * 2, 5));
            }
        }

        // Ensure admin can log in even if the DB was previously seeded
        // (older seed versions may have created admin without PasswordHash).
        var adminEmail = "admin@whale.local";
        var existingAdmin = await db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == adminEmail);
        if (existingAdmin is not null)
        {
            if (string.IsNullOrWhiteSpace(existingAdmin.PasswordHash))
            {
                existingAdmin.PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!");
                await db.SaveChangesAsync();
            }

            return;
        }

        // If already seeded (but no admin), do nothing
        if (await db.Users.AnyAsync())
            return;

        // 1) Users
        var admin = new User
        {
            Email = "admin@whale.local",
            FullName = "Whale Admin",
            Role = UserRole.Admin,
            Status = UserStatus.Active,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!")
        };

        var teacher1 = new User
        {
            Email = "teach1@whale.local",
            FullName = "Teacher One",
            Role = UserRole.Teacher,
            Status = UserStatus.Active
        };

        var teacher2 = new User
        {
            Email = "teach2@whale.local",
            FullName = "Teacher Two",
            Role = UserRole.Teacher,
            Status = UserStatus.Active
        };

        var student1 = new User
        {
            Email = "stud1@whale.local",
            FullName = "Student One",
            Role = UserRole.Student,
            Status = UserStatus.Active
        };

        var student2 = new User
        {
            Email = "stud2@whale.local",
            FullName = "Student Two",
            Role = UserRole.Student,
            Status = UserStatus.Active
        };

        db.Users.AddRange(admin, teacher1, teacher2, student1, student2);
        await db.SaveChangesAsync();

        // 2) Teacher profiles
        db.TeacherProfiles.AddRange(
            new TeacherProfile
            {
                UserId = teacher1.Id,
                VerificationStatus = TeacherVerificationStatus.Approved,
                JoinedAtUtc = DateTime.UtcNow.AddDays(-20)
            },
            new TeacherProfile
            {
                UserId = teacher2.Id,
                VerificationStatus = TeacherVerificationStatus.Pending,
                JoinedAtUtc = DateTime.UtcNow.AddDays(-5)
            }
        );

        // 3) Courses
        db.Courses.AddRange(
            new Course
            {
                TeacherUserId = teacher1.Id,
                Title = "Intro to Whale Testing",
                Description = "A starter course for QA/testing mindset.",
                Status = CourseStatus.Published,
                Price = 9.99m
            },
            new Course
            {
                TeacherUserId = teacher1.Id,
                Title = "PostgreSQL for Humans",
                Description = "DB fundamentals with practical examples.",
                Status = CourseStatus.Draft,
                Price = null
            }
        );

        await db.SaveChangesAsync();
    }

    private static bool IsTransientDbStartupException(Exception ex)
    {
        if (ex is NpgsqlException)
        {
            return true;
        }

        if (ex is SocketException)
        {
            return true;
        }

        return ex.InnerException is SocketException;
    }
}
