using Clinicia.Common.Helpers;
using Clinicia.Dtos.Common;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;
using CorePush.Google;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Clinicia.Services.Implementations
{
    public class FcmService : IPushNotificationService
    {
        private readonly AppSettings _appSettings;

        private readonly IUnitOfWork _unitOfWork;

        public FcmService(IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task SendAsync(string deviceToken, FcmPayloadNotification payload)
        {
            using (var apn = new FcmSender(_appSettings.FCMServerKey, _appSettings.FCMSenderID))
            {
                await apn.SendAsync(deviceToken, payload);
            }
        }

        public async Task SendToAllDeviceUsers(Guid userId, FcmPayloadNotification payload)
        {
            var devices = _unitOfWork.DeviceRepository.Get(x => x.UserId == userId && x.IsActive && x.ExpiredAt > DateTime.UtcNow);
            using (var apn = new FcmSender(_appSettings.FCMServerKey, _appSettings.FCMSenderID))
            {
                foreach (var device in devices)
                {
                    await apn.SendAsync(device.DeviceToken, payload);
                }
            }
        }
    }
}
