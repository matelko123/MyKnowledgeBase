using System.Linq.Expressions;

namespace MyKnowledgeBase.Optimization.Pagination.PaginatedList;

public static class PaginatedListSorting
{
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string orderByMember, bool? isSortingAsc = true)
    {
        var queryElementTypeParam = Expression.Parameter(typeof(T));
        var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);
        var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

        isSortingAsc ??= true;

        var orderBy = Expression.Call(
            typeof(Queryable),
            isSortingAsc == true ? "OrderBy" : "OrderByDescending",
            new Type[] { typeof(T), memberAccess.Type },
            query.Expression,
            Expression.Quote(keySelector));

        return query.Provider.CreateQuery<T>(orderBy);
    }
}
