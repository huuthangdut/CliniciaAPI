using Clinicia.Dtos.Common;
using System;
using System.Threading.Tasks;

namespace Clinicia.Services.Interfaces
{
    public interface IPushNotificationService
    {
        Task SendAsync(string deviceToken, FcmPayloadNotification payload);

        Task SendToAllDeviceUsers(Guid userId, FcmPayloadNotification payload);
    }
}
