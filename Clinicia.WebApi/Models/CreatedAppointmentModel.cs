using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class CreatedAppointmentModel
    {
        [Required]
        public long AppointmentDate { get; set; }

        [Required]
        public int DurationInMinutes { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public string DoctorId { get; set; }
    }
}
