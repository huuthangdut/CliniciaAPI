using System;

namespace Clinicia.Dtos.Input
{
    public class CreatedCheckingService
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int DurationInMinutes { get; set; }

        public decimal Price { get; set; }

        public Guid DoctorId { get; set; }
    }

    public class UpdatedCheckingService
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DurationInMinutes { get; set; }

        public decimal Price { get; set; }
    }
}
