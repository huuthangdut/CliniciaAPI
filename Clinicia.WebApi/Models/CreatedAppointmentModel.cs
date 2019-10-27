namespace Clinicia.WebApi.Models
{
    public class CreatedAppointmentModel
    {
        public long AppointmentDate { get; set; }

        public int DurationInMinutes { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }

        public string DoctorId { get; set; }
    }
}
