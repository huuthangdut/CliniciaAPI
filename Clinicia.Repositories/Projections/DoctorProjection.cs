using System;
using Clinicia.Repositories.Schemas;

namespace Clinicia.Repositories.Projections
{
    public class DoctorProjection
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool? Gender { get; set; }

        public int? YearExperience { get; set; }

        public string Awards { get; set; }

        public string About { get; set; }

        public string ImageProfile { get; set; }

        public string MedicalSchool { get; set; }

        public string Clinic { get; set; }

        public decimal? Price { get; set; }

        public DbLocation Location { get; set; }

        public DbSpecialty Specialty { get; set; }

        public decimal? Rating { get; set; }

        public int? RatingCount { get; set; }

        public decimal? DistanceFromPatient { get; set; }

        public bool? AvailableToday { get; set; }
    }
}
