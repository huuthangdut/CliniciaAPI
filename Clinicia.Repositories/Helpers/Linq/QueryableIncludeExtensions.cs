using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Clinicia.Repositories.Helpers.Linq
{
    public static class QueryableIncludeExtensions
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(
                    query,
                    (current, include) => current.Include(include));
            }

            return query;
        }

        public static IQueryable<T> IncludeMultipleIf<T>(this IQueryable<T> query, bool condition, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            if (condition && includes != null)
            {
                query = includes.Aggregate(
                    query,
                    (current, include) => current.Include(include));
            }

            return query;
        }


        public static IQueryable<TCollection> IncludeIf<TCollection, TProperty>(
            this IQueryable<TCollection> source,
            bool condition,
            Expression<Func<TCollection, TProperty>> path) where TCollection : class
        {
            return condition ? source.Include(path) : source;
        }

        public static IQueryable<TCollection> Includes<TCollection, TEntity, TProperty>(
            this IQueryable<TCollection> source,
            Expression<Func<TCollection, TEntity>> entityExpression,
            Expression<Func<TEntity, TProperty>> pathExpression) where TCollection : class
        {
            return source.Include((entityExpression == null ? string.Empty : ParsePath(entityExpression) + ".") + ParsePath(pathExpression));
        }

        private static string ParsePath<T, TProperty>(Expression<Func<T, TProperty>> path)
        {
            string parsedPath;
            if (!TryParsePath(path.Body, out parsedPath) || parsedPath == null)
            {
                throw new ArgumentException("The Include path expression must refer to a navigation property defined on the type. Use dotted paths for reference navigation properties and the Select operator for collection navigation properties.", "path");
            }
            return parsedPath;
        }

        // Decompiled System.Data.Entity.Internal.DbHelpers.TryParsePath
        private static bool TryParsePath(Expression expression, out string path)
        {
            path = null;
            expression = expression.RemoveConvert();
            var memberExpression = expression as MemberExpression;
            var methodCallExpression = expression as MethodCallExpression;
            if (memberExpression != null)
            {
                string parsedPath;
                var name = memberExpression.Member.Name;
                if (!TryParsePath(memberExpression.Expression, out parsedPath))
                {
                    return false;
                }
                path = parsedPath == null ? name : parsedPath + "." + name;
            }
            else if (methodCallExpression != null)
            {
                if (methodCallExpression.Method.Name == "Select" && methodCallExpression.Arguments.Count == 2)
                {
                    string parsedPath;
                    if (!TryParsePath(methodCallExpression.Arguments[0], out parsedPath))
                    {
                        return false;
                    }
                    if (parsedPath != null)
                    {
                        var lambdaExpression = methodCallExpression.Arguments[1] as LambdaExpression;
                        if (lambdaExpression != null)
                        {
                            string lambdaParsedPath;
                            if (!TryParsePath(lambdaExpression.Body, out lambdaParsedPath))
                            {
                                return false;
                            }
                            if (lambdaParsedPath != null)
                            {
                                path = parsedPath + "." + lambdaParsedPath;
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            return true;
        }

        private static Expression RemoveConvert(this Expression expression)
        {
            while (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked)
            {
                expression = ((UnaryExpression)expression).Operand;
            }
            return expression;
        }
    }
}