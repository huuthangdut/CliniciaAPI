using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class CreatedCheckingServiceModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int DurationInMinutes { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string DoctorId { get; set; }
    }

    public class UpdatedCheckingServiceModel : CreatedCheckingServiceModel
    {
        [Required]
        public string Id { get; set; }
    }
}
