using Clinicia.Infrastructure.ApiControllers;
using Clinicia.Services.Interfaces;
using Clinicia.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clinicia.WebApi.Controllers.V1
{
    [Authorize]
    public class DevicesController : BaseApiController
    {
        private readonly IDeviceService _deviceService;
        private readonly IPushNotificationService pushNotificationService;

        public DevicesController(IDeviceService deviceService, IPushNotificationService pushNotificationService)
        {
            _deviceService = deviceService;
            this.pushNotificationService = pushNotificationService;
        }

        [HttpPost]
        public IActionResult Post(DeviceModel model)
        {
            _deviceService.AddOrUpdateDevice(UserId, model.DeviceUuid, model.DeviceType, model.DeviceToken);

            return Success();
        }

        [HttpPost("{deviceUuid}/status")]
        public ActionResult Status([FromRoute] string deviceUuid, [FromBody] StatusModel<bool> model)
        {
            _deviceService.UpdateStatus(UserId, deviceUuid, model.Status);

            return Success();
        }
    }
}
