using Clinicia.Common;

namespace Clinicia.Repositories.Projections
{
    public class DoctorTimeProjection
    {
        public TimeRange[] WorkingHoursInDay { get; set; }

        public TimeRange[] TimeOffInDay { get; set; }

        public TimeRange[] TimeBusyInDay { get; set; }
    }
}