using Microsoft.EntityFrameworkCore;
using SportStore.Application.DTOs;

namespace SportStore.Application.Extensions
{
    public static class PaginationExtensions
    {
        public static async Task<PageResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<T>(items, count, pageNumber, pageSize);
        }
    }
}
