namespace Whale.Application.Common;

public record PagedResult<T>(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyList<T> Items
);
