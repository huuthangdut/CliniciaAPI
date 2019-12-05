using System;

namespace Clinicia.Dtos.Input
{
    public class FilterWorktime
    {
        public TimeSpan? TimeFrom { get; set; }

        public int? ServiceDuration { get; set; }
    }
}
