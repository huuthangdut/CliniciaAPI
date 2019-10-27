using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class FavoriteModel
    {
        [Required]
        public string DoctorId { get; set; }
    }
}
