namespace Whale.Domain.Entities;

public enum UserRole
{
    Admin = 1,
    Teacher = 2,
    Student = 3
}

public enum UserStatus
{
    Active = 1,
    Inactive = 2,
    Suspended = 3,
    Left = 4
}

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;

    public UserRole Role { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Active;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? LastSeenAtUtc { get; set; }
}
