using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Whale.Application.Admin.Dto;
using Whale.Application.Admin.Queries;
using Whale.Application.Common;
using Whale.Infrastructure.Persistence;

namespace Whale.Api.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/admin/courses")]
public class AdminCoursesController : ControllerBase
{
    private readonly WhaleDbContext _db;
    public AdminCoursesController(WhaleDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<PagedResult<CourseListItemDto>>> Get([FromQuery] AdminListQuery q)
    {
        var page = q.Page < 1 ? 1 : q.Page;
        var pageSize = q.PageSize < 1 ? 20 : q.PageSize;
        if (pageSize > 100) pageSize = 100;

        var query = _db.Courses
            .AsNoTracking()
            .Include(c => c.TeacherUser)
            .AsQueryable();

        // Search: course title
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim().ToLower();
            query = query.Where(c => c.Title.ToLower().Contains(s));
        }

        // Status: Draft / Published / Archived
        if (!string.IsNullOrWhiteSpace(q.Status))
        {
            var status = q.Status.Trim();
            query = query.Where(c => c.Status.ToString() == status);
        }

        var total = await query.CountAsync();

        var items = await query
            .OrderByDescending(c => c.CreatedAtUtc)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new CourseListItemDto(
                c.Id,
                c.Title,
                c.Status.ToString(),
                c.Price,
                c.CreatedAtUtc,
                c.TeacherUserId,
                c.TeacherUser.Email,
                c.TeacherUser.FullName
            ))
            .ToListAsync();

        return Ok(new PagedResult<CourseListItemDto>(page, pageSize, total, items));
    }
}
