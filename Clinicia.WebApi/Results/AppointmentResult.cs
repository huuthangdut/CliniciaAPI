namespace Clinicia.WebApi.Results
{
    public class AppointmentResult
    {
        public string Id { get; set; }

        public long AppointmentDate { get; set; }

        public int TotalMinutes { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string PublicResult { get; set; }

        public int Type { get; set; }

        public int Status { get; set; }

        public int SendNotificationBeforeMinutes { get; set; }

        public AppointmentDoctorResult Doctor { get; set; }

        public DoctorCheckingServiceResult CheckingService { get; set; }
    }

    public class AppointmentDoctorResult
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageProfile { get; set; }

        public string Address { get; set; }

        public string Clinic { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
