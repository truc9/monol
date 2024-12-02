using Microsoft.EntityFrameworkCore;

namespace Share;

public static class QueryableExtensions
{
    public static async Task<PagingResult<T>> ToPagingAsync<T>(this IQueryable<T> query, int pageNumber = 1,
        int pageSize = 25, CancellationToken cancellationToken = default) where T : class
    {
        var total = await query
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var data = await query
            .AsNoTracking()
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagingResult<T>(data, pageNumber, pageSize, total);
    }
}