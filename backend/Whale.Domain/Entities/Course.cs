namespace Whale.Domain.Entities;

public enum CourseStatus
{
    Draft = 1,
    Published = 2,
    Archived = 3
}

public class Course
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TeacherUserId { get; set; }
    public User TeacherUser { get; set; } = null!;

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public CourseStatus Status { get; set; } = CourseStatus.Draft;

    public decimal? Price { get; set; } // null = free
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
