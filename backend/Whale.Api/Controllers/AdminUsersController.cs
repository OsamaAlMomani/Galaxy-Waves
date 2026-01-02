using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Whale.Application.Admin.Dto;
using Whale.Application.Admin.Queries;
using Whale.Application.Common;
using Whale.Infrastructure.Persistence;

namespace Whale.Api.Controllers;

[ApiController][Authorize(Roles = "Admin")]

[Route("api/admin/users")]
public class AdminUsersController : ControllerBase
{
    private readonly WhaleDbContext _db;
    public AdminUsersController(WhaleDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<PagedResult<UserListItemDto>>> Get([FromQuery] AdminListQuery q)
    {
        var page = q.Page < 1 ? 1 : q.Page;
        var pageSize = q.PageSize is < 1 ? 20 : q.PageSize;
        if (pageSize > 100) pageSize = 100;

        var query = _db.Users.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim().ToLower();
            query = query.Where(x =>
                x.Email.ToLower().Contains(s) ||
                x.FullName.ToLower().Contains(s));
        }

        if (!string.IsNullOrWhiteSpace(q.Status))
        {
            var status = q.Status.Trim();
            query = query.Where(x => x.Status.ToString() == status);
        }

        var total = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.CreatedAtUtc)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new UserListItemDto(
                x.Id,
                x.Email,
                x.FullName,
                x.Role.ToString(),
                x.Status.ToString(),
                x.CreatedAtUtc,
                x.LastSeenAtUtc
            ))
            .ToListAsync();

        return Ok(new PagedResult<UserListItemDto>(page, pageSize, total, items));
    }
}
