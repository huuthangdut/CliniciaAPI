using Clinicia.Dtos.Common;
using Clinicia.Infrastructure.ApiControllers;
using Clinicia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Clinicia.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class ValuesController : BaseApiController
    {
        private readonly IPushNotificationService pushNotificationService;

        public IUserService PatientService { get; }

        public ValuesController(IUserService patientService, IPushNotificationService pushNotificationService)
        {
            PatientService = patientService;
            this.pushNotificationService = pushNotificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await pushNotificationService.SendAsync("dJDZ5g_scFg:APA91bFPJeRCinee3WKWFBvPradWvUG91VU-xg6M6HTBPpw9z4x5N-9hDh6bzNtbLCY3eSA8PVcGaaVgy60bccOTjNgMxlSJg_ZhRPJx1NLNxp1UEGsf3ByWsp9Ldmc4WRMSGicPXSCh", new FcmPayloadNotification
            {
                Notification = new FcmNotification
                {
                    Title = "Test",
                    Body = "content",
                },
                Data = new FcmDataNotification
                {
                    Id = "guid",
                    Image = null,
                    NotificationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ")
                }
            });

            return Success();
        }
    }
}