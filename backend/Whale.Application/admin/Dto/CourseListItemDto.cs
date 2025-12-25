namespace Whale.Application.Admin.Dto;

public record CourseListItemDto(
    Guid Id,
    string Title,
    string Status,
    decimal? Price,
    DateTime CreatedAtUtc,
    Guid TeacherUserId,
    string TeacherEmail,
    string TeacherFullName
);
