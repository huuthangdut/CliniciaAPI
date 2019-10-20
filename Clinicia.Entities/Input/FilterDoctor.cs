using Clinicia.Common.Enums;

namespace Clinicia.Dtos.Input
{
    public class FilterDoctor
    {
        public string SearchTerm { get; set; }

        public double PatientLongitude { get; set; }

        public double PatientLatitude { get; set; }

        public bool? Gender { get; set; }

        public int? YearExperience { get; set; }

        public Symbol? FilterYearExperienceSymbol { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }

        public bool? AvailableToday { get; set; }
    }
}