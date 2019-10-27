using System;

namespace Clinicia.Dtos.Output
{
    public class Appointment
    {
        public Guid Id { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int DurationInMinutes { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string PublicResult { get; set; }

        public int Type { get; set; }

        public int Status { get; set; }

        public AppointmentDoctor Doctor { get; set; }
    }

    public class AppointmentDoctor
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Clinic { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
