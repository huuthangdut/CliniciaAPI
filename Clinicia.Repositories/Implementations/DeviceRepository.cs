using Clinicia.Repositories.Interfaces;
using Clinicia.Repositories.Schemas;

namespace Clinicia.Repositories.Implementations
{
    public class DeviceRepository 
        : GenericRepository<DbDeviceUser>, IDeviceRepository
    {
        public DeviceRepository(CliniciaDbContext context) 
            : base(context)
        {
        }
    }
}
