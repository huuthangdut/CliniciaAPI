using Clinicia.Common.Helpers;
using Clinicia.Dtos.Common;
using Clinicia.Services.Interfaces;
using CorePush.Google;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Clinicia.Services.Implementations
{
    public class FcmService : IPushNotificationService
    {
        private readonly AppSettings _appSettings;

        public FcmService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task SendAsync(string deviceToken, FcmPayloadNotification payload)
        {
            using (var apn = new FcmSender(_appSettings.FCMServerKey, _appSettings.FCMSenderID))
            {
                await apn.SendAsync(deviceToken, payload);
            }
        }
    }
}
