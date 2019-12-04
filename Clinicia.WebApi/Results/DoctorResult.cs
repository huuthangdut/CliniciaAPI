using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;

namespace Clinicia.WebApi.Results
{
    public class DoctorResult
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageProfile { get; set; }

        public bool? Gender { get; set; }

        public decimal? Price { get; set; }

        public string MedicalSchool { get; set; }

        public string Awards { get; set; }

        public string About { get; set; }

        public int? YearExperience { get; set; }

        public string Clinic { get; set; }

        public decimal? Rating { get; set; }

        public int? RatingCount { get; set; }

        public double DistanceFromPatient { get; set; }

        public Location Location { get; set; }

        public DictionaryItem Specialty { get; set; }
    }
}
