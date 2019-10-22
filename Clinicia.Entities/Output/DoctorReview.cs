using Clinicia.Dtos.Common;
using System;

namespace Clinicia.Dtos.Output
{
    public class DoctorReview
    {
        public Guid Id { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; }

        public DictionaryItem Patient { get; set; }
    }
}
