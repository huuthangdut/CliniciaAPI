using System;

namespace Clinicia.Common.Helpers
{
    public static class DateTimeConvertHelper
    {
        public const long EpochTicks = 621355968000000000;
        public const long TicksPeriod = 10000000;
        public const long TicksPeriodMs = 10000;

        public static readonly DateTime Epoch = new DateTime(EpochTicks, DateTimeKind.Utc);

        public static long ToMilliSecondsTimestamp(this DateTime date)
        {
            long ts = (date.Ticks - EpochTicks) / TicksPeriodMs;
            return ts;
        }

        public static long ToMilliSecondsTimestamp(this DateTime? date)
        {
            return date == null ? 0 : ToMilliSecondsTimestamp(date);
        }

        public static long ToSecondsTimestamp(this DateTime date)
        {
            if (date.Year == 9999)
            {
                return 0;
            }

            long ts = (date.Ticks - EpochTicks) / TicksPeriod;
            return ts;
        }

        public static DateTime LocalToUtcTime(this DateTime date)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(date, TimeZoneInfo.Local.Id, TimeZoneInfo.Utc.Id);
        }

        public static DateTime UtcToLocalTime(this DateTime date)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(date, TimeZoneInfo.Utc.Id, TimeZoneInfo.Local.Id);
        }

        public static long ToSecondsTimestamp(this DateTime? date)
        {
            return date == null ? 0 : (date.Value.Ticks - EpochTicks) / TicksPeriod;
        }

        public static long ToRoundedSecondsTimestamp(this DateTime date, long factor)
        {
            return (ToSecondsTimestamp(date) / factor) * factor;
        }

        public static DateTime FromUnixTimeStamp(this long unixTimeStamp)
        {
            return Epoch.AddSeconds(unixTimeStamp);
        }

        public static DateTime? FromUnixTimeStamp(this long? unixTimeStamp)
        {
            return unixTimeStamp > 0 ? FromUnixTimeStamp(unixTimeStamp.Value) : (DateTime?)null;
        }
    }
}