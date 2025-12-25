namespace Whale.Application.Admin.Dto;

public record TeacherListItemDto(
    Guid TeacherProfileId,
    Guid UserId,
    string Email,
    string FullName,
    string VerificationStatus,
    DateTime JoinedAtUtc,
    DateTime? LeftAtUtc
);
