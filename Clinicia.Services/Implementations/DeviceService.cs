using Clinicia.Common.Helpers;
using Clinicia.Repositories.Schemas;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;

namespace Clinicia.Services.Implementations
{
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly AppSettings _appSettings;

        public DeviceService(IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
        }

        public void AddOrUpdateDevice(Guid userId, string deviceUuid, string deviceType, string deviceToken)
        {
            var device = _unitOfWork.DeviceRepository.GetFirstOrDefault(x => x.UserId == userId && x.DeviceUuid == deviceUuid);

            if (device == null)
            {
                var dbDeviceUser = new DbDeviceUser
                {
                    UserId = userId,
                    DeviceUuid = deviceUuid,
                    DeviceType = deviceType,
                    DeviceToken = deviceToken,
                    ExpiredAt = DateTime.UtcNow.AddHours(_appSettings.AccessTokenLifeTimeHours),
                    IsActive = true
                };

                _unitOfWork.DeviceRepository.Add(dbDeviceUser);
                _unitOfWork.Complete();
                return;
            }

            device.DeviceToken = deviceToken;
            device.IsActive = true;
            device.ExpiredAt = DateTime.UtcNow.AddHours(_appSettings.AccessTokenLifeTimeHours);

            _unitOfWork.Complete();
        }

        public void UpdateStatus(Guid userId, string deviceUuid, bool status)
        {
            var device = _unitOfWork.DeviceRepository.GetFirstOrDefault(x => x.DeviceUuid == deviceUuid && x.UserId == userId);
            if(device.IsActive != status)
            {
                device.IsActive = status;

                _unitOfWork.Complete();
            }
        }
    }
}
