using System;

namespace Clinicia.Dtos.Output
{
    public class ReminderAppointment
    {
        public Guid UserId { get; set; }

        public string DoctorName { get; set; }

        public string DoctorImage { get; set; }

        public DateTime AppointmentDate { get; set; }

        public Device[] Devices { get; set; }
    }
}
