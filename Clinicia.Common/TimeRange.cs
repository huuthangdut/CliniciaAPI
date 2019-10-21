using Clinicia.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clinicia.Common
{
    public class TimeRange
    {
        public TimeSpan From { get; set; }

        public TimeSpan To { get; set; }

        public TimeRange(TimeSpan from, TimeSpan to)
        {
            From = from;
            To = to;
        }

        public TimeRange(string from, string to)
        {
            From = from.ToTimeSpan();
            To = to.ToTimeSpan();
        }
    }

    public static class TimeRangeUtils
    {
        public static TimeSpan[] GetTimeFrame(TimeRange timeRange, TimeRange[] breakTimes, TimeRange[] extraBreakTimes, TimeRange filter = null, int timeOffsetMinutes = 30)
        {
            // has filter
            if (filter != null)
            {
                if (filter.From > timeRange.To || filter.To < timeRange.From || filter.To < filter.From)
                {
                    return null;
                }

                if (filter.From > timeRange.From)
                {
                    timeRange.From = filter.From;
                }

                if (filter.To < timeRange.To)
                {
                    timeRange.To = filter.To;
                }
            }

            var breakTimes_ = breakTimes.Select(x => new TimeRange(x.From, x.To)).ToArray();
            var extraBreakTimes_ = extraBreakTimes.Select(x => new TimeRange(x.From, x.To)).ToArray();

            // merge and reduce break times
            var breaks = new List<TimeRange>()
                .ConcatIfNotNullOrEmpty(breakTimes_)
                .ConcatIfNotNullOrEmpty(extraBreakTimes_)
                .Where(time => ReduceTime(time, timeRange))
                .OrderBy(time => time.From).ThenBy(time => time.To)
                .ToArray();

            var offset = new TimeSpan(0, timeOffsetMinutes, 0);
            var timeFrame = new List<TimeSpan>();

            // has break time
            if (!breaks.IsNullOrEmpty())
            {
                if (timeRange.From < breaks[0].From)
                {
                    for (var time = timeRange.From; time <= breaks[0].From.Subtract(offset); time = time.Add(offset))
                    {
                        timeFrame.Add(time);
                    }
                }

                for (int i = 0; i < breaks.Length - 1; i++)
                {
                    for (var time = breaks[i].To; time <= breaks[i + 1].From.Subtract(offset); time = time.Add(offset))
                    {
                        timeFrame.Add(time);
                    }
                }

                if (breaks[breaks.Length - 1].To < timeRange.To)
                {
                    for (var time = breaks[breaks.Length - 1].To; time <= timeRange.To.Subtract(offset); time = time.Add(offset))
                    {
                        timeFrame.Add(time);
                    }
                }
            }
            else
            {
                for (var time = timeRange.From; time <= timeRange.To.Subtract(offset); time = time.Add(offset))
                {
                    timeFrame.Add(time);
                }
            }

            return timeFrame.ToArray();
        }

        private static bool ReduceTime(TimeRange timeToReduce, TimeRange timeRange)
        {
            if (timeToReduce.From < timeRange.From)
            {
                timeToReduce.From = timeRange.From;
            }

            if (timeToReduce.To > timeRange.To)
            {
                timeToReduce.To = timeRange.To;
            }

            return !(timeToReduce.To < timeRange.From || timeToReduce.From > timeRange.To);
        }
    }
}