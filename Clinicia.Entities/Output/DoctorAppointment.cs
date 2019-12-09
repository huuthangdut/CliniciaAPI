using System;

namespace Clinicia.Dtos.Output
{
    public class DoctorAppointment
    {
        public Guid Id { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int TotalMinutes { get; set; }

        public decimal TotalPrice { get; set; }

        public int Type { get; set; }

        public int Status { get; set; }

        public AppointmentPatient Patient { get; set; }

        public DoctorCheckingService CheckingService { get; set; }
    }

    public class AppointmentPatient
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageProfile { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string PhoneNumber { get; set; }
    }
}
