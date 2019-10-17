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
        public static async Task<PagedResult<T>> GetPagedResultAsync<T>(
            this IQueryable<T> source,
            int pageIndex,
            int pageSize,
            object extraData = null)
        {
            if (pageSize == 0)
            {
                return EmptyPagedResult<T>.Instance;
            }

            var count = await source.CountAsync().ConfigureAwait(false);

            var items = await source
                .PageBy(pageIndex * pageSize, pageSize)
                .MakeQueryToDatabaseAsync()
                .ConfigureAwait(false);

            return new PagedResult<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = count,
                Items = items.ToArray(),
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                ExtraData = extraData,
            };
        }

        public static async Task<PagedResult<TEntityDto>> GetPagedResultAsync<TProjection, TEntityDto>(
            this IQueryable<TProjection> source,
            int pageIndex,
            int pageSize,
            Func<TProjection, TEntityDto> convert)
        {
            var count = await source.CountAsync().ConfigureAwait(false);

            var items = source
                .PageBy(pageIndex * pageSize, pageSize)
                .MakeQueryToDatabase();

            return new PagedResult<TEntityDto>
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