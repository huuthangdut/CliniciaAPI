using AutoMapper;
using Clinicia.Common.Helpers;
using Clinicia.Infrastructure.ApiControllers;
using Clinicia.Services.Interfaces;
using Clinicia.WebApi.Models;
using Clinicia.WebApi.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinicia.WebApi.Controllers.V1
{
    [Authorize]
    public class WorkingScheduleController : BaseApiController
    {
        private readonly IWorkingScheduleService _workingScheduleService;

        private readonly IMapper _mapper;

        public WorkingScheduleController(IWorkingScheduleService workingScheduleService, IMapper mapper)
        {
            _workingScheduleService = workingScheduleService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetWorkingSchedule()
        {
            var result = _workingScheduleService.GetWorkingSchedules(UserId);

            return Success(_mapper.Map<WorkingScheduleResult[]>(result));
        }

        [HttpPost("{id}/hour")]
        public IActionResult UpdateWorkingHours([FromRoute] string id, [FromBody] WorkingHourModel model)
        {
            _workingScheduleService.UpdateWorkingHour(id.ParseGuid(), model.Hours);

            return Success();
        }


    }
}
