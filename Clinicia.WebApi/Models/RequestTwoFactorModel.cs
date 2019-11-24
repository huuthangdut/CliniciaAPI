using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class RequestTwoFactorModel
    {
        [Required]
        public string PhoneNumber { get; set; }
    }
}
