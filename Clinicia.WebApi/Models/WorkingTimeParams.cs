using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class WorkingTimeParams
    {
        [Required]
        public string Date { get; set; }

        public string TimeFrom { get; set; }

        public int ServiceDuration { get; set; }
    }
}
