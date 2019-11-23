using AutoMapper;
using Clinicia.Common.Helpers;
using Clinicia.Dtos.Common;
using Clinicia.Infrastructure.ApiControllers;
using Clinicia.Services.Interfaces;
using Clinicia.WebApi.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clinicia.WebApi.Controllers.V1
{
    [Authorize]
    public class NotificationsController : BaseApiController
    {
        private readonly INotificationService _notificationService;

        private readonly IMapper _mapper;

        public NotificationsController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page, int pageSize)
        {
            var result = await _notificationService.GetNotificationsAsync(page, pageSize, UserId);

            return Success(_mapper.Map<PagedResult<NotificationResult>>(result));
        }

        [HttpPost("{id}/read")]
        public IActionResult Read([FromRoute] string id)
        {
            _notificationService.MarkAsRead(id.ParseGuid());

            return Success();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _notificationService.DeleteAsync(id.ParseGuid());

            return Success();
        }
    }
}
