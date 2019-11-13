namespace Clinicia.WebApi.Results
{
    public class DoctorCheckingServiceResult
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DurationInMinutes { get; set; }

        public decimal Price { get; set; }
    }
}
