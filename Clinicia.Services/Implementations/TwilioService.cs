using Clinicia.Common.Enums;
using Clinicia.Common.Exceptions;
using Clinicia.Common.Helpers;
using Clinicia.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;

namespace Clinicia.Services.Implementations
{
    public class TwilioService : ISmsService
    {
        private readonly AppSettings _appSettings;

        public TwilioService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task SendAsync(string message, string phoneTo)
        {
            try
            {
                TwilioClient.Init(_appSettings.TwilioSid, _appSettings.TwilioAuthToken);

                var messageResource = await MessageResource.CreateAsync(
                    body: message,
                    from: new Twilio.Types.PhoneNumber(_appSettings.TwilioPhoneFrom),
                    to: new Twilio.Types.PhoneNumber(phoneTo)
                );

                if(messageResource.Status == MessageResource.StatusEnum.Failed)
                {
                    throw new BusinessException(ErrorCodes.Failed.ToString(), messageResource.ErrorMessage);
                }
            }
            catch (TwilioException ex)
            {
                throw new BusinessException(ErrorCodes.Failed.ToString(), ex.Message);
            }
        }
    }
}
