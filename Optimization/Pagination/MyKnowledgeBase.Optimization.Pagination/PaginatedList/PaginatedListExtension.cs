using Microsoft.EntityFrameworkCore;

namespace MyKnowledgeBase.Optimization.Pagination.PaginatedList;

public static class PaginatedListExtension
{
    public static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable,
        PaginatedListRequest options,
        CancellationToken cancellationToken)
        where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), options, cancellationToken);

    public static PaginatedList<TDestination> ToPaginatedList<TDestination>(
       this IQueryable<TDestination> queryable,
       PaginatedListRequest options)
       where TDestination : class
       => PaginatedList<TDestination>.Create(queryable.AsNoTracking(), options);
}