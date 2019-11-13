using System;

namespace Clinicia.Dtos.Input
{
    public class CreatedAppointment
    {
        public DateTime AppointmentDate { get; set; }

        public int TotalMinutes { get; set; }

        public decimal TotalPrice { get; set; }

        public Guid CheckingServiceId { get; set; }

        public Guid DoctorId { get; set; }
    }
}
