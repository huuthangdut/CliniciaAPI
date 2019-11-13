using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class CreatedAppointmentModel
    {
        [Required]
        public string AppointmentDate { get; set; }

        [Required]
        public int TotalMinutes { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public string CheckingServiceId { get; set; }

        [Required]
        public string DoctorId { get; set; }
    }
}
