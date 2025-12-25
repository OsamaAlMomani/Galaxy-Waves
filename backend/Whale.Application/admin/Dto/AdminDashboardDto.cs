namespace Whale.Application.Admin.Dto;

public record AdminDashboardDto(
    int TotalUsers,
    int ActiveUsers,
    int TotalTeachers,
    int PendingTeachers,
    int ApprovedTeachers,
    int TotalCourses,
    int PublishedCourses,
    int DraftCourses
);
