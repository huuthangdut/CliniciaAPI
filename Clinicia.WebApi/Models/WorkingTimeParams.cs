using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class WorkingTimeParams
    {
        [Required]
        public string Date { get; set; }
    }
}
