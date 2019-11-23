using System;

namespace Clinicia.WebApi.Results
{
    public class NotificationResult
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public bool HasRead { get; set; }

        public long NotificationDate { get; set; }
    }
}
