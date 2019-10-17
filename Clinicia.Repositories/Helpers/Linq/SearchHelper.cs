using System;
using System.Linq;
using System.Linq.Expressions;
using Clinicia.Common.Extensions;

namespace Clinicia.Repositories.Helpers.Linq
{
    public static class SearchHelper
    {
        public static IQueryable<TEntity> SearchByFields<TEntity>(this IQueryable<TEntity> query, string text, params Expression<Func<TEntity, string>>[] fields)
        {
            var tokens = GetTokensFromString(text);
            return query.SearchByFields(tokens, fields);
        }

        public static IQueryable<TEntity> SearchByFields<TEntity>(this IQueryable<TEntity> query, string[] tokens, params Expression<Func<TEntity, string>>[] fields)
        {
            if (tokens.IsNullOrEmpty() || fields.IsNullOrEmpty())
            {
                return query;
            }

            var whereExpression = tokens.Aggregate<string, MethodCallExpression>(null, (current, token) => query.CombineWhereExpression(current, field => field.Contains(token), fields));
            return query.Provider.CreateQuery<TEntity>(whereExpression);
        }

        public static IQueryable<TEntity> SearchByEqualFields<TEntity>(this IQueryable<TEntity> query, string[] tokens, params Expression<Func<TEntity, string>>[] fields)
        {
            if (tokens.IsNullOrEmpty() || fields.IsNullOrEmpty())
            {
                return query;
            }

            var whereExpression = tokens.Aggregate<string, MethodCallExpression>(null, (current, token) => query.CombineWhereExpression(current, field => field.Equals(token), fields));
            return query.Provider.CreateQuery<TEntity>(whereExpression);
        }

        private static MethodCallExpression CombineWhereExpression<TEntity>(this IQueryable<TEntity> query, MethodCallExpression previousExpr, Expression<Func<string, bool>> testExpression, params Expression<Func<TEntity, string>>[] fields)
        {
            var parameter = Expression.Parameter(typeof(TEntity));
            var fieldTestPredicates = fields.Select(getField => getField.Apply(testExpression, parameter));
            var testPredicate = fieldTestPredicates.Aggregate((firstExpr, secondExpr) => firstExpr == null ? secondExpr : firstExpr.OrElse(secondExpr));

            return Expression.Call(
                typeof(Queryable),
                "Where",
                new[] { query.ElementType },
                previousExpr ?? query.Expression,
                testPredicate);
        }

        private static Expression<Func<T, bool>> Apply<T>(this Expression<Func<T, string>> memberSelector, Expression<Func<string, bool>> testExpression, ParameterExpression sharedParam)
        {
            var memberExpression = (MemberExpression)memberSelector.Body;
            var methodCallExpression = (MethodCallExpression)testExpression.Body;

            MemberExpression memberAccess = null;

            var innerMemberExpression = memberExpression.Expression as MemberExpression;
            if (innerMemberExpression != null)
            {
                var innerMemberType = memberExpression.Member.DeclaringType;
                if (innerMemberType != null)
                {
                    var innerParameter = Expression.Parameter(innerMemberType);
                    var innerMemberAccess = Expression.MakeMemberAccess(innerParameter, memberExpression.Member);

                    memberAccess = Expression.MakeMemberAccess(Expression.MakeMemberAccess(sharedParam, innerMemberExpression.Member), innerMemberAccess.Member);
                }
            }

            memberAccess = memberAccess ?? Expression.MakeMemberAccess(sharedParam, memberExpression.Member);

            var call = Expression.Call(memberAccess, methodCallExpression.Method, methodCallExpression.Arguments);

            return Expression.Lambda<Func<T, bool>>(call, sharedParam);
        }

        private static Expression<Func<T, T1>> OrElse<T, T1>(this Expression<Func<T, T1>> first, Expression<Func<T, T1>> second)
        {
            return Expression.Lambda<Func<T, T1>>(Expression.OrElse(first.Body, second.Body), first.Parameters);
        }

        public static string[] GetTokensFromString(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new string[0];
            }

            if (text.Contains(","))
            {
                return text
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Distinct()
                    .ToArray();
            }

            return text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
        }
    }
}