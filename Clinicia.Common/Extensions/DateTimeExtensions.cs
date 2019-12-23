using Clinicia.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Clinicia.Common.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts a DateTime to a Unix Timestamp
        /// </summary>
        /// <param name="target">This DateTime</param>
        /// <returns></returns>
        public static double ToUnixTimestamp(this DateTime target)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var diff = target - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /// <summary>
        /// Converts a Unix Timestamp in to a DateTime
        /// </summary>
        /// <param name="unixTime">This Unix Timestamp</param>
        /// <returns></returns>
        public static DateTime FromUnixTimestamp(this double unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return epoch.AddSeconds(unixTime);
        }

        /// <summary>
        /// Gets the value of the Start of the day (00:00)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime ToDayStart(this DateTime target)
        {
            return target.Date;
        }

        /// <summary>
        /// Gets the value of the End of the day (23:59)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime ToDayEnd(this DateTime target)
        {
            return target.Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// Gets the First Date of the week for the specified date
        /// </summary>
        /// <param name="dt">this DateTime</param>
        /// <param name="startOfWeek">The Start Day of the Week (ie, Sunday/Monday)</param>
        /// <returns>The First Date of the week</returns>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            var diff = dt.DayOfWeek - startOfWeek;

            if (diff < 0)
                diff += 7;

            return dt.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Returns all the days of a month.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> DaysOfMonth(int year, int month)
        {
            return Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                .Select(day => new DateTime(year, month, day + 1));
        }

        /// <summary>
        /// Determines the Nth instance of a Date's DayOfWeek in a month
        /// </summary>
        /// <returns></returns>
        /// <example>11/29/2011 would return 5, because it is the 5th Tuesday of each month</example>
        public static int WeekDayInstanceOfMonth(this DateTime dateTime)
        {
            var y = 0;
            return DaysOfMonth(dateTime.Year, dateTime.Month)
                .Where(date => dateTime.DayOfWeek.Equals(date.DayOfWeek))
                .Select(
                    x => new
                    {
                        n = ++y,
                        date = x
                    })
                .Where(x => x.date.Equals(new DateTime(dateTime.Year, dateTime.Month, dateTime.Day)))
                .Select(x => x.n)
                .FirstOrDefault();
        }

        public static int WeekOfYear(this DateTime dateTime)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            return cal.GetWeekOfYear(
                dateTime,
                dfi.CalendarWeekRule,
                dfi.FirstDayOfWeek);
        }

        public static int QuarterOfYear(this DateTime dateTime)
        {
            var month = dateTime.Month;
            if (month.IsBetween(1, 3))
            {
                return 1;
            }

            if (month.IsBetween(4, 6))
            {
                return 2;
            }

            if (month.IsBetween(7, 9))
            {
                return 3;
            }

            if (month.IsBetween(10, 12))
            {
                return 4;
            }

            return 0;
        }

        /// <summary>
        /// Gets the total days in a month
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static int TotalDaysInMonth(this DateTime dateTime)
        {
            return DaysOfMonth(dateTime.Year, dateTime.Month).Count();
        }

        /// <summary>
        /// Takes any date and returns it's value as an Unspecified DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeUnspecified(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
            {
                return date;
            }

            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Takes any date and returns it's value as an Unspecified DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeLocal(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
            {
                return date;
            }

            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Local);
        }

        /// <summary>
        /// Trims the milliseconds off of a datetime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime TrimMilliseconds(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
        }

        /// <summary>
        /// Get the start day of a month
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetStartDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0);
        }

        /// <summary>
        /// Get the end day of a month
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetEndDayOfMonth(this DateTime dateTime)
        {
            return GetStartDayOfMonth(dateTime).AddMonths(1).AddDays(-1).ToDayEnd();
        }

        /// <summary>
        /// Compare months and years of 2 DateTime params
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool CompareMonthOfDate(this DateTime date1, DateTime date2)
        {
            if (date1.Month == date2.Month && date1.Year == date2.Year)
            {
                return true;
            }
            return false;
        }

        public static TimeSpan ToTimeSpan(this string value)
        {
            try
            {
                return DateTime.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None).TimeOfDay;
            }
            catch
            {
               throw new FormatException();
            }
        }

        public static TimeRange[] ToWorkingTimes(this string hours, DayOfWeek dayOfWeek)
        {
            try
            {
                var hour = hours.Split(',');
                var hourOfDay = hour[(int)dayOfWeek];
                var timeRangeOfDay = hourOfDay.Split('+');
                var timeRangeResult = new List<TimeRange>();
                foreach (var timeRange in timeRangeOfDay)
                {
                    var time = timeRange.Split('-');
                    if (time.Length == 2)
                    {
                        var timeFrom = time[0];
                        var timeTo = time[1];
                        timeRangeResult.Add(new TimeRange(timeFrom, timeTo));
                    }
                }

                return timeRangeResult.ToArray();
            }
            catch
            {
                throw new FormatException();
            }
        }

        public static Dictionary<DayOfWeek, TimeRange[]> ToWeekWorkingTimes(this string hours)
        {
            try
            {
                var dictionary = new Dictionary<DayOfWeek, TimeRange[]>();
                var hour = hours.Split(',');
                for (int i = 0; i < hour.Length; i++)
                {
                    var timeRangeOfDay = hour[i].Split('+');
                    var timeRangeResult = new List<TimeRange>();
                    foreach (var timeRange in timeRangeOfDay)
                    {
                        var time = timeRange.Split('-');
                        if (time.Length == 2)
                        {
                            var timeFrom = time[0];
                            var timeTo = time[1];   
                            timeRangeResult.Add(new TimeRange(timeFrom, timeTo));
                        }
                    }

                    dictionary[i.ToString().ParseEnum<DayOfWeek>()] = timeRangeResult.ToArray();
                }


                return dictionary;
            }
            catch
            {
                throw new FormatException();
            }
        }

        public static DateTime RoundUp(this DateTime dt, TimeSpan d)
        {
            if (dt.Hour == 23 && dt.Minute > (60 - d.TotalMinutes))
            {
                return dt.RoundDown(d);
            }

            var modTicks = dt.Ticks % d.Ticks;
            var delta = modTicks != 0 ? d.Ticks - modTicks : 0;
            return new DateTime(dt.Ticks + delta, dt.Kind);
        }

        public static DateTime RoundDown(this DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            return new DateTime(dt.Ticks - delta, dt.Kind);
        }
    }
}