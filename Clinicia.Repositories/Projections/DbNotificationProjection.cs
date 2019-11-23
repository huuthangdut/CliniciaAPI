using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;

namespace Clinicia.Repositories.Projections
{
    public class DbNotificationProjection : DbNotification
    {
        public Device[] Devices { get; set; }
    }
}
