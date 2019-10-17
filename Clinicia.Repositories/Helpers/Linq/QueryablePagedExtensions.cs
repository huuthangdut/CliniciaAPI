using Clinicia.Common.Extensions;
using Clinicia.Entities.Common;
using Clinicia.Repositories.Helpers.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Helpers.PagedResult
{
    public static class QueryablePagedAndSortedExtensions
    {
        public static async Task<PagedResult<TResult>> GetPagedResultAsync<T, TResult>(
            this IQueryable<T> source,
            Func<IQueryable<T>, IOrderedQueryable<T>> setOrderFunction,
            int pageIndex,
            int pageSize,
            Func<T, TResult> convert)
        {
            if (pageSize == 0)
            {
                return EmptyPagedResult<TResult>.Instance;
            }

            var count = await source.CountAsync().ConfigureAwait(false);

            var items = await setOrderFunction(source)
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