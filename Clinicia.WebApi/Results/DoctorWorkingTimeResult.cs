﻿namespace Clinicia.WebApi.Results
{
    public class DoctorWorkingTimeResult
    {
        public string DoctorId { get; set; }

        public long WorkingDate { get; set; }

        public string[] WorkingTimes { get; set; }
    }
}