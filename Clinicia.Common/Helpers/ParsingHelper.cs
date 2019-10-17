using System;
using System.Globalization;
using System.Linq;
using Clinicia.Common.Exceptions;
using Newtonsoft.Json;

namespace Clinicia.Common.Helpers
{
    public static class ParsingHelper
    {
        private static readonly string[] QueryParameterArraySeparator = new[] { "," };

        public static int[] ParseIntArray(this string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? EmptyArray<int>.Instance
                : value.Split(QueryParameterArraySeparator, StringSplitOptions.RemoveEmptyEntries).Select(ParseInt).ToArray();
        }

        public static int[] ParseIdsSerialized(this string idsSerialized)
        {
            return string.IsNullOrEmpty(idsSerialized)
                ? EmptyArray<int>.Instance
                : JsonConvert.DeserializeObject<string[]>(idsSerialized).ParseIds();
        }

        public static int ParseId(this string value)
        {
            var result = value.ParseInt();
            if (result <= 0)
            {
                throw new InvalidArgumentException();
            }

            return result;
        }

        public static int? ParseNullableId(this string value)
        {
            return value.ParseNullableInt();
        }

        public static int ParseInt(this string value)
        {
            int result;
            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            throw new InvalidArgumentException();
        }

        public static decimal ParseDecimal(this string value)
        {
            decimal result;
            if (!string.IsNullOrWhiteSpace(value) && decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            throw new InvalidArgumentException();
        }

        public static decimal? ParseNullableDecimal(this string value)
        {
            return value.ParseNullableDecimal(null);
        }

        public static decimal ParseNullableDecimal(this string value, decimal @default)
        {
            return value.ParseNullableDecimal((decimal?)@default).Value;
        }

        private static decimal? ParseNullableDecimal(this string value, decimal? @default)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return @default;
            }

            decimal result;
            if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            throw new InvalidArgumentException();
        }

        public static int? ParseNullableInt(this string value)
        {
            return value.ParseNullableInt(null);
        }

        public static int ParseNullableInt(this string value, int @default)
        {
            return value.ParseNullableInt((int?)@default).Value;
        }

        private static int? ParseNullableInt(this string value, int? @default)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return @default;
            }

            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }

            throw new InvalidArgumentException();
        }

        public static int[] ParseIds(this string[] value)
        {
            return value == null ? EmptyArray<int>.Instance : value.Select(x => x.ParseId()).ToArray();
        }

        public static bool ParseBool(this string value)
        {
            var result = ParseNullableBool(value);
            if (result.HasValue)
            {
                return result.Value;
            }

            throw new InvalidArgumentException();
        }

        public static bool? ParseNullableBool(this string value)
        {
            bool result;
            if (!string.IsNullOrWhiteSpace(value) && bool.TryParse(value, out result))
            {
                return result;
            }

            return null;
        }

        public static bool ParseNullableBool(this string value, bool @default)
        {
            return ParseNullableBool(value) ?? @default;
        }

        public static Guid? ParseNullableGuid(this string value)
        {
            return value.ParseNullableGuid(null);
        }

        private static Guid? ParseNullableGuid(this string value, Guid? @default)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return @default;
            }

            Guid result;
            if (Guid.TryParse(value, out result))
            {
                return result;
            }

            throw new InvalidArgumentException();
        }

        public static Guid ParseGuid(this string value)
        {
            Guid result;
            if (!string.IsNullOrWhiteSpace(value) && Guid.TryParse(value, out result))
            {
                return result;
            }

            throw new InvalidArgumentException();
        }

        public static DateTime ParseDate(this string value, string dateFormat)
        {
            var result = value.TryParseDate(dateFormat);
            if (result.HasValue)
            {
                return result.Value;
            }

            throw new InvalidArgumentException();
        }

        public static DateTime? TryParseDate(this string value, string dateFormat)
        {
            DateTime result;
            if (!string.IsNullOrWhiteSpace(value) && DateTime.TryParseExact(value, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out result))
            {
                return result;
            }

            return null;
        }

        public static TEnum ParseEnum<TEnum>(this string value) where TEnum : struct
        {
            TEnum result;
            if (Enum.TryParse(value, true, out result))
            {
                return result;
            }
            throw new InvalidArgumentException();
        }
    }
}