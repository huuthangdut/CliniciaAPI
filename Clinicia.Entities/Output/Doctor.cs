﻿using Clinicia.Dtos.Common;

namespace Clinicia.Dtos.Output
{
    public class Doctor : User
    {
        public string MedicalSchool { get; set; }

        public string Awards { get; set; }

        public int? YearExperience { get; set; }

        public string Clinic { get; set; }

        public decimal? Rating { get; set; }

        public int? RatingCount { get; set; }

        public double DistanceFromPatient { get; set; }

        public Location Location { get; set; }

        public DictionaryItem Specialty { get; set; }
    }
}
