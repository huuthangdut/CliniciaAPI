using System;

namespace Clinicia.WebApi.Results
{
    public class DoctorAppointmentResult
    {
        public string Id { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int TotalMinutes { get; set; }

        public decimal Price { get; set; }

        public int Type { get; set; }

        public int Status { get; set; }

        public AppointmentPatientResult Patient { get; set; }

        public DoctorCheckingServiceResult CheckingService { get; set; }
    }

    public class AppointmentPatientResult
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageProfile { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string PhoneNumber { get; set; }
    }
}
