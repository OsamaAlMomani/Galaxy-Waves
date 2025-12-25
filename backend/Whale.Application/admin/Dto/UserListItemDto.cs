namespace Whale.Application.Admin.Dto;

public record UserListItemDto(
    Guid Id,
    string Email,
    string FullName,
    string Role,
    string Status,
    DateTime CreatedAtUtc,
    DateTime? LastSeenAtUtc
);
