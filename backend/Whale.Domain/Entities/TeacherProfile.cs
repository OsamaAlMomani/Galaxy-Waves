namespace Whale.Domain.Entities;

public enum TeacherVerificationStatus
{
    Pending = 1,
    Approved = 2,
    Rejected = 3
}

public class TeacherProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public TeacherVerificationStatus VerificationStatus { get; set; } = TeacherVerificationStatus.Pending;

    public DateTime JoinedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? LeftAtUtc { get; set; }
}
