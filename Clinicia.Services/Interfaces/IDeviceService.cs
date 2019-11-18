using System;

namespace Clinicia.Services.Interfaces
{
    public interface IDeviceService
    {
        void AddOrUpdateDevice(Guid userId, string deviceUuid, string deviceType, string deviceToken);

        void UpdateStatus(Guid userId, string deviceUuid, bool status);
    }
}
