using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;
using System;

namespace Clinicia.Repositories.Projections
{
    public class DbNotificationProjection : DbNotification
    {
        public Device[] Devices { get; set; }

        public Guid AppointmentId { get; set; }
    }
}
