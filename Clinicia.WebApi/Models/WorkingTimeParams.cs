using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class WorkingTimeParams
    {
        [Required]
        public long? Date { get; set; }
    }
}
