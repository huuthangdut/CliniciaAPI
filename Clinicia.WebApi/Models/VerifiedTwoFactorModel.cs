using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class VerifiedTwoFactorModel
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
