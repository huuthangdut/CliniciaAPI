using System;

namespace Clinicia.Dtos.Input
{
    public class CreatedAppointment
    {
        public DateTime AppointmentDate { get; set; }

        public int DurationInMinutes { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }

        public Guid DoctorId { get; set; }
    }
}
