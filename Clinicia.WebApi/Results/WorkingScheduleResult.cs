using Clinicia.Common;
using System;
using System.Collections.Generic;

namespace Clinicia.WebApi.Results
{
    public class WorkingScheduleResult
    {
        public string Id { get; set; }

        public DateTime FromDate { get; set; }

        public Dictionary<int, TimeRangeResult[]> Hours { get; set; }

        public bool IsActive { get; set; }
    }
}
