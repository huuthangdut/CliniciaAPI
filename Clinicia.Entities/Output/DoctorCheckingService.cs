using System;

namespace Clinicia.Dtos.Output
{
    public class DoctorCheckingService
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DurationInMinutes { get; set; }

        public decimal Price { get; set; }
    }
}
