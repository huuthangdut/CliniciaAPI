using System.Collections.Generic;

namespace Clinicia.Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static TEntity GetValueOrNull<TKey, TEntity>(this IDictionary<TKey, TEntity> dictionary, TKey key) where TEntity : class
        {
            return dictionary.GetValueOrDefault(key, null);
        }

        public static TEntity GetValueOrDefault<TKey, TEntity>(this IDictionary<TKey, TEntity> dictionary, TKey key, TEntity @default)
        {
            TEntity value;
            return (key != null && dictionary.TryGetValue(key, out value)) ? value : @default;
        }

        public static string Convert(this IDictionary<string, string> dictionary, string key)
        {
            return dictionary.GetValueOrDefault(key, string.Empty);
        }
    }
}