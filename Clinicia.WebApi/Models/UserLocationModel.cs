using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class UserLocationModel
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public string FormattedAddress { get; set; }
    }
}
