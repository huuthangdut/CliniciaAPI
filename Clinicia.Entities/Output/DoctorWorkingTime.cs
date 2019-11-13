using System;

namespace Clinicia.Dtos.Output
{
    public class DoctorWorkingTime
    {
        public Guid DoctorId { get; set; }

        public TimeSpan[] WorkingTimes { get; set; }
    }
}
