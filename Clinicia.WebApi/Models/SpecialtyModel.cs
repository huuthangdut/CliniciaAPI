using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class SpecialtyModel
    {
        [Required]
        public string SpecialtyId { get; set; }
    }
}
