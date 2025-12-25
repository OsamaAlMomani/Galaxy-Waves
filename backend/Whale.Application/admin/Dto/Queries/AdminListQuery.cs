namespace Whale.Application.Admin.Queries;

public record AdminListQuery(
    int Page = 1,
    int PageSize = 20,
    string? Search = null,
    string? Status = null
);
