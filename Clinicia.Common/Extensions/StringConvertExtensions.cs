using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
    }
}