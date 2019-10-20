using System;
using System.Linq;
using System.Threading.Tasks;
using Clinicia.Common.Extensions;
using Clinicia.Dtos.Common;
using Microsoft.EntityFrameworkCore;

namespace Clinicia.Repositories.Helpers.Linq
{
    public static class QueryablePagedAndSortedExtensions
    {
        public static async Task<PagedResult<TResult>> GetPagedResultAsync<T, TResult>(
            this IQueryable<T> source,
            int pageIndex,
            int pageSize,
            Func<T, TResult> convert)
        {
            if (pageSize == 0)
            {
                return EmptyPagedResult<TResult>.Instance;
            }

            var count = await source.CountAsync().ConfigureAwait(false);

            var items = await source
                .PageBy(pageIndex * pageSize, pageSize)
                .MakeQueryToDatabaseAsync()
                .ConfigureAwait(false);

            return new PagedResult<TResult>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = count,
                Items = items.ConvertArray(convert),
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
        }
    }
}