using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Whale.Application.Admin.Dto;
using Whale.Application.Admin.Queries;
using Whale.Application.Common;
using Whale.Infrastructure.Persistence;

namespace Whale.Api.Controllers;

[ApiController][Authorize(Roles = "Admin")]

[Route("api/admin/teachers")]
public class AdminTeachersController : ControllerBase
{
    private readonly WhaleDbContext _db;
    public AdminTeachersController(WhaleDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<PagedResult<TeacherListItemDto>>> Get([FromQuery] AdminListQuery q)
    {
        var page = q.Page < 1 ? 1 : q.Page;
        var pageSize = q.PageSize < 1 ? 20 : q.PageSize;
        if (pageSize > 100) pageSize = 100;

        var query = _db.TeacherProfiles
            .AsNoTracking()
            .Include(t => t.User)
            .AsQueryable();

        // Search: matches teacher email or full name
        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim().ToLower();
            query = query.Where(t =>
                t.User.Email.ToLower().Contains(s) ||
                t.User.FullName.ToLower().Contains(s));
        }

        // Status: verification status (Pending/Approved/Rejected)
        if (!string.IsNullOrWhiteSpace(q.Status))
        {
            var status = q.Status.Trim();
            query = query.Where(t => t.VerificationStatus.ToString() == status);
        }

        var total = await query.CountAsync();

        var items = await query
            .OrderByDescending(t => t.JoinedAtUtc)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TeacherListItemDto(
                t.Id,
                t.UserId,
                t.User.Email,
                t.User.FullName,
                t.VerificationStatus.ToString(),
                t.JoinedAtUtc,
                t.LeftAtUtc
            ))
            .ToListAsync();

        return Ok(new PagedResult<TeacherListItemDto>(page, pageSize, total, items));
    }
}
