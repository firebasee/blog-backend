using Microsoft.EntityFrameworkCore;

namespace Blog.SharedKernel;

public static class QueryableExtensions
{
    public static PagedResult<T> ToPagedResult<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        var totalCount = query.LongCount();
        var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PagedResult<T>(items,pageNumber,pageSize,totalCount);
    }

    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var totalCount =await query.LongCountAsync(cancellationToken: cancellationToken);
        var items =await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken: cancellationToken);
        return new PagedResult<T>(items,pageNumber,pageSize,totalCount);
    }
}