using System;
using System.Collections.Generic;
using System.Text;

namespace Clinicia.Dtos.Output
{
    public class Notification
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public bool HasRead { get; set; }

        public DateTime NotificationDate { get; set; }

        public Guid? AppointmentId { get; set; }
    }
}
