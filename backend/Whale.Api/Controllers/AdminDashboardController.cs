using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Whale.Application.Admin.Dto;
using Whale.Infrastructure.Persistence;

namespace Whale.Api.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/admin/dashboard")]
public class AdminDashboardController : ControllerBase
{
    private readonly WhaleDbContext _db;
    public AdminDashboardController(WhaleDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<AdminDashboardDto>> Get()
    {
        var totalUsers = await _db.Users.CountAsync();
        var activeUsers = await _db.Users.CountAsync(u => u.Status.ToString() == "Active");

        var totalTeachers = await _db.TeacherProfiles.CountAsync();
        var pendingTeachers = await _db.TeacherProfiles.CountAsync(t => t.VerificationStatus.ToString() == "Pending");
        var approvedTeachers = await _db.TeacherProfiles.CountAsync(t => t.VerificationStatus.ToString() == "Approved");

        var totalCourses = await _db.Courses.CountAsync();
        var publishedCourses = await _db.Courses.CountAsync(c => c.Status.ToString() == "Published");
        var draftCourses = await _db.Courses.CountAsync(c => c.Status.ToString() == "Draft");

        return Ok(new AdminDashboardDto(
            totalUsers,
            activeUsers,
            totalTeachers,
            pendingTeachers,
            approvedTeachers,
            totalCourses,
            publishedCourses,
            draftCourses
        ));
    }
}
