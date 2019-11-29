using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class StatusModel
    {
        [Required]
        public bool Status { get; set; }
    }
}
