using System;
using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class CreatedReviewModel
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public Guid DoctorId { get; set; }

        public Guid AppointmentId { get; set; }
    }
}
