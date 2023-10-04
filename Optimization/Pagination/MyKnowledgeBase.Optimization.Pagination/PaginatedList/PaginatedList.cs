using Microsoft.EntityFrameworkCore;

namespace MyKnowledgeBase.Optimization.Pagination.PaginatedList;

public class PaginatedList<T>
{
    public IReadOnlyCollection<T> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    private PaginatedList(IReadOnlyCollection<T> items, int count, PaginatedListRequest options)
    {
        PageNumber = options.PageNumber.GetValueOrDefault(PaginatedListRequest.DefaultPage);
        TotalPages = (int)Math.Ceiling(count / (double)options.PageSize.GetValueOrDefault(PaginatedListRequest.DefaultPageSize));
        TotalCount = count;
        Items = items;
    }

    // TODO: Add ordering 
    public static async Task<PaginatedList<T>> CreateAsync(
        IQueryable<T> source, 
        PaginatedListRequest options,
        CancellationToken cancellationToken)
    {
        var count = await source.CountAsync(cancellationToken);
        var pageNumber = options.PageNumber.GetValueOrDefault(PaginatedListRequest.DefaultPage);
        var pageSize = options.PageSize.GetValueOrDefault(PaginatedListRequest.DefaultPageSize);

        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<T>(items, count, options);
    }

    public static PaginatedList<T> Create(
        IQueryable<T> source,
        PaginatedListRequest options)
    {
        var count = source.Count();
        var pageNumber = options.PageNumber.GetValueOrDefault(PaginatedListRequest.DefaultPage);
        var pageSize = options.PageSize.GetValueOrDefault(PaginatedListRequest.DefaultPageSize);

        if (!string.IsNullOrEmpty(options.SortField))
        {
            source = source.OrderByDynamic<T>(options.SortField!, options.IsSortingAsc);
        }

        var items = source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginatedList<T>(items, count, options);
    }
}