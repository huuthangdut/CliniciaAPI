using System;

namespace Clinicia.Common.Timing.Timezone
{
    /// <summary>
    /// Time zone converter class
    /// </summary>
    public class TimeZoneConverter : ITimeZoneConverter
    {
        private static readonly string DefaultUserTimezone = string.Empty;

        public DateTime? Convert(DateTime? date)
        {
            if (!date.HasValue)
            {
                return null;
            }

            if (!Clock.SupportsMultipleTimezone)
            {
                return date;
            }

            var applicationsTimezone = DefaultUserTimezone;
            if (string.IsNullOrEmpty(applicationsTimezone))
            {
                return date;
            }

            return TimezoneHelper.ConvertFromUtc(date.Value.ToUniversalTime(), applicationsTimezone);
        }
    }
}