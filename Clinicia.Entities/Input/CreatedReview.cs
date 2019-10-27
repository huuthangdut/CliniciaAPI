using System;

namespace Clinicia.Dtos.Input
{
    public class CreatedReview
    {
        public int Rating { get; set; }

        public string Comment { get; set; }

        public Guid DoctorId { get; set; }
    }
}