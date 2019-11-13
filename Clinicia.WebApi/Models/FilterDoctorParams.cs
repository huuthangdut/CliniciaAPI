namespace Clinicia.WebApi.Models
{
    public class FilterDoctorParams
    {
        public string SpecialtyId { get; set; }

        public string SearchTerm { get; set; }

        public bool? Gender { get; set; }

        public string YearExperience { get; set; }

        public string Price { get; set; }

        public bool? AvailableToday { get; set; }
    }
}
