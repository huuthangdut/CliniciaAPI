using System;

namespace Clinicia.Dtos.Output
{
    public class DoctorWorkingTime
    {
        public Guid DoctorId { get; set; }

        public DateTime WorkingDate { get; set; }

        public TimeSpan[] WorkingTimes { get; set; }
    }
}
