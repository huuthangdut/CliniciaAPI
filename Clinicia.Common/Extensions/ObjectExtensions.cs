using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Clinicia.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static T As<T>(this object obj)
            where T : class
        {
            return (T)obj;
        }

        public static PropertyInfo GetPropertyEndWith(this object value, string propertyNamePrefix)
        {
            return value.GetType()
                .GetProperties()
                .FirstOrDefault(pi => pi.Name.EndsWith(propertyNamePrefix, StringComparison.InvariantCulture));
        }

        public static TResult NullSafeGetValue<TSource, TResult>(this TSource source, Expression<Func<TSource, TResult>> expression)
        {
            var value = GetValue(expression, source);
            return value == null ? default(TResult) : (TResult)value;
        }

        public static TResult NullSafeGetValue<TSource, TResult>(this TSource source, Expression<Func<TSource, TResult>> expression, TResult defaultValue)
        {
            var value = GetValue(expression, source);
            return value == null ? defaultValue : (TResult)value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TCastResultType"></typeparam>
        /// <param name="source">Root Object</param>
        /// <param name="expression">Labmda expression to set the property value returned</param>
        /// <param name="defaultValue">The default value in the case the property is not reachable</param>
        /// <param name="convertToResultToAction">An action to cast the returned value</param>
        /// <returns></returns>
        public static TCastResultType NullSafeGetValue<TSource, TResult, TCastResultType>(this TSource source, Expression<Func<TSource, TResult>> expression, TCastResultType defaultValue, Func<object, TCastResultType> convertToResultToAction)
        {
            var value = GetValue(expression, source);
            return value == null ? defaultValue : convertToResultToAction.Invoke(value);
        }

        private static string GetFullPropertyPathName<TSource, TResult>(Expression<Func<TSource, TResult>> expression)
        {
            return expression.Body.ToString().Replace(expression.Parameters[0] + ".", string.Empty);
        }

        private static object GetValue<TSource, TResult>(Expression<Func<TSource, TResult>> expression, TSource source)
        {
            string fullPropertyPathName = GetFullPropertyPathName(expression);
            return GetNestedPropertyValue(fullPropertyPathName, source);
        }

        private static object GetNestedPropertyValue(string name, object obj)
        {
            PropertyInfo info;
            foreach (var part in name.Split('.'))
            {
                if (obj == null)
                {
                    return null;
                }

                var type = obj.GetType();
                if (obj is IEnumerable)
                {
                    type = (obj as IEnumerable).GetType();
                    var methodInfo = type.GetMethod("get_Item");
                    var index = int.Parse(part.Split('(')[1].Replace(")", string.Empty));
                    try
                    {
                        obj = methodInfo.Invoke(obj, new object[] { index });
                    }
                    catch (Exception)
                    {
                        obj = null;
                    }
                }
                else
                {
                    info = type.GetProperty(part);
                    if (info == null)
                    {
                        return null;
                    }

                    obj = info.GetValue(obj, null);
                }
            }

            return obj;
        }
    }
}