using Clinicia.Common;
using System;
using System.Collections.Generic;

namespace Clinicia.Dtos.Output
{
    public class WorkingSchedule
    {
        public string Id { get; set; }

        public DateTime FromDate { get; set; }

        public Dictionary<DayOfWeek, TimeRange[]> Hours { get; set; }

        public bool IsActive { get; set; }
    }
}
