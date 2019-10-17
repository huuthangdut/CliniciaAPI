using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Clinicia.Common;
using Microsoft.EntityFrameworkCore;

namespace Clinicia.Repositories.Helpers.Linq
{
    public static class QueryableExtensions
    {
        public static T[] MakeQueryToDatabase<T>(this IQueryable<T> source)
        {
            return source.ToArray();
        }

        public static async Task<T[]> MakeQueryToDatabaseAsync<T>(this IQueryable<T> source)
        {
            return await source.ToArrayAsync();
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(EfPredicateBuilder.True<T>().And(predicate))
                : query;
        }

        public static IQueryable<TEntity> OrderByIf<TEntity, T>(this IQueryable<TEntity> query, bool condition, Expression<Func<TEntity, T>> keySelector)
        {
            return condition
                ? query.OrderBy(keySelector)
                : query;
        }

        public static IQueryable<TEntity> OrderByDescendingIf<TEntity, T>(this IQueryable<TEntity> query, bool condition, Expression<Func<TEntity, T>> keySelector)
        {
            return condition
                ? query.OrderByDescending(keySelector)
                : query;
        }

        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
        {
            Guard.NotNull(query, nameof(query));

            return query.Skip(skipCount).Take(maxResultCount);
        }
    }
}