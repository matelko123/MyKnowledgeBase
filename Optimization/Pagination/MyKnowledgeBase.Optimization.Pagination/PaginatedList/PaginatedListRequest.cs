namespace MyKnowledgeBase.Optimization.Pagination.PaginatedList;

public abstract class PaginatedListRequest
{
    public const int DefaultPage = 1;
    public const int DefaultPageSize = 10;

    public int? PageNumber { get; init; } = DefaultPage;
    public int? PageSize { get; init; } = DefaultPageSize;

    public string? SortField { get; set; }
    public bool? IsSortingAsc { get; set; } = true;
}