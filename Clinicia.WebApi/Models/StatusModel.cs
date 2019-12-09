using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class StatusModel<T>
    {
        [Required]
        public T Status { get; set; }
    }
}
