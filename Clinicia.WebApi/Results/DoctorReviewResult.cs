using Clinicia.Dtos.Common;

namespace Clinicia.WebApi.Results
{
    public class DoctorReviewResult
    {
        public string Id { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public long ReviewDate { get; set; }

        public DictionaryItem Patient { get; set; }
    }
}