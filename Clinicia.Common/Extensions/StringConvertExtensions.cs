using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Clinicia.Common.Enums;
using Clinicia.Common.Exceptions;
using Clinicia.Common.Helpers;

namespace Clinicia.Common.Extensions
{
    public static class StringConvertExtensions
    {
        private const string MoneyFormat = "0.00";
        private const string MoneyMorePrecisionFormat = "0.0000";

        private const string ShortFormat = "0.00";

        public static string SafeToString(this object value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        public static string ToStringInvariant(this bool value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToStringInvariant(this int? value)
        {
            return value == null ? string.Empty : value.Value.ToStringInvariant();
        }

        public static string ToStringInvariant(this int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToStringInvariant(this long value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToStringInvariant(this decimal value)
        {
            return value.ToString(ShortFormat, CultureInfo.InvariantCulture);
        }

        public static string ToStringInvariant(this decimal? value)
        {
            return value == null ? string.Empty : value.Value.ToString(ShortFormat, CultureInfo.InvariantCulture);
        }

        public static string ToStringInvariant(this DateTime value, string format)
        {
            return value.ToString(format, CultureInfo.InvariantCulture);
        }

        public static string ToStringInvariant(this DateTime? value, string format)
        {
            return value == null ? string.Empty : value.Value.ToStringInvariant(format);
        }

        public static string ToStringInvariant(this Guid? value)
        {
            return value == null ? string.Empty : value.Value.ToStringInvariant();
        }

        public static string ToStringInvariant(this Guid value)
        {
            return value.ToString();
        }

        public static string ToShortStringInvariant(this float? value)
        {
            return value.HasValue ? value.Value.ToString(ShortFormat, CultureInfo.InvariantCulture) : string.Empty;
        }

        public static string ToShortStringInvariant(this decimal? value)
        {
            return value.HasValue ? value.Value.ToShortStringInvariant() : string.Empty;
        }

        public static string ToShortStringInvariant(this decimal value)
        {
            return value.ToString(ShortFormat, CultureInfo.InvariantCulture);
        }

        public static string ToMoneyString(this decimal value)
        {
            return value.ToMoneyString(true);
        }

        public static string ToMoneyString(this decimal? value)
        {
            return value.HasValue ? value.Value.ToMoneyString(true) : string.Empty;
        }

        public static string ToQuantityString(this string id, int? quantity)
        {
            return quantity.HasValue && quantity > 1 ? id + " (x" + quantity.Value + ")" : id;
        }

        public static string ToPaymentTypeDetailsString(this string paymentType, string details)
        {
            return paymentType + (string.IsNullOrEmpty(details) ? string.Empty : " (" + details + ")");
        }

        public static string ToMoneyString(this decimal value, bool useDimensionCharacter)
        {
            return value.ToMoneyString(useDimensionCharacter, MoneyFormat);
        }

        public static string ToMoneyStringWithMorePrecision(this decimal value)
        {
            return value.ToMoneyString(true, MoneyMorePrecisionFormat);
        }

        public static string ToMoneyString(this decimal value, bool useDimensionCharacter, string moneyFormat)
        {
            return (value < 0 ? "-" : string.Empty) + (useDimensionCharacter ? "$" : string.Empty) + (value < 0 ? -value : value).ToString(moneyFormat, CultureInfo.InvariantCulture);
        }

        public static string JoinNotEmpty(this IEnumerable<string> values, string separator)
        {
            return string.Join(separator, values.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        public static string ToSeparateLinesString(this IEnumerable<string> values)
        {
            return string.Join(Environment.NewLine, values.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        public static string WrapWithBrackets(this string value)
        {
            return string.Format("({0})", value);
        }

        public static string ToStringInvariant(this bool? value)
        {
            return value.HasValue ? value.Value.ToStringInvariant() : string.Empty;
        }

        public static SortOrder? GetSortOrder(this string expression)
        {
            if (string.IsNullOrEmpty(expression) || expression.Length <= 1)
            {
                return null;
            }

            var sortSymbol = expression[0];
            switch (sortSymbol)
            {
                case '+':
                    return SortOrder.Asc;
                case '-':
                    return SortOrder.Desc;
                default:
                    return null;
            }
        }

        public static Symbol? GetCompareSymbol(this string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return null;
            }

            var compareSymbol = expression[0];
            switch (compareSymbol)
            {
                case '>':
                    return Symbol.GreaterThan;
                case '<':
                    return Symbol.LessThan;
                case '=':
                default:
                    return Symbol.Equal;
            }
        }

        public static (double Lat, double Lng) GetLatLng(this string value)
        {
            var latLng = value.Split(',');

            try
            {
                var lat = double.Parse(latLng[0]);
                var lng = double.Parse(latLng[1]);

                return (Lat: lat, Lng: lng);
            }
            catch
            {
                throw new InvalidArgumentException();
            }
        }

        public static (decimal? PriceFrom, decimal? PriceTo) GetPriceRange(this string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return (null, null);
            }

            try
            {
                var priceRange = expression.Split(',');
                var priceFrom = decimal.TryParse(priceRange[0], out var priceFromResult) ? (decimal?)priceFromResult : null;
                var priceTo = decimal.TryParse(priceRange[1], out var priceToResult) ? (decimal?)priceToResult : null;

                return (PriceFrom: priceFrom, PriceTo: priceTo);
            }
            catch
            {
                throw new InvalidArgumentException();
            }
        }

        public static int? ExtractValue(this string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return null;
            }

            if (!char.IsDigit(expression[0]))
            {
                return int.TryParse(expression.Substring(1, expression.Length - 1), out var result1)
                    ? (int?)result1
                    : null;
            }

            return int.TryParse(expression, out var result2)
                ? (int?)result2
                : null;
        }



        public static SortOptions<TEnum> ToSortOptions<TEnum>(this string value) where TEnum : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            try
            {
                value = value.Trim();

                if (!char.IsLetterOrDigit(value[0]))
                {
                    return new SortOptions<TEnum>
                    {
                        SortByField = value.Substring(1, value.Length - 1).ParseEnum<TEnum>(),
                        SortOrder = value[0] == '+'
                            ? SortOrder.Asc
                            : (value[0] == '-' ? SortOrder.Desc : throw new InvalidArgumentException())
                    };
                }
                return new SortOptions<TEnum>
                {
                    SortByField = value.ParseEnum<TEnum>(),
                    SortOrder = SortOrder.Asc
                };
            }
            catch
            {
                throw new InvalidArgumentException();
            }
            
        }
    }
}