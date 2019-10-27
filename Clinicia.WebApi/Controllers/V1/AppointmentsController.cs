using AutoMapper;
using Clinicia.Common.Helpers;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Input;
using Clinicia.Infrastructure.ApiControllers;
using Clinicia.Services.Interfaces;
using Clinicia.WebApi.Models;
using Clinicia.WebApi.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clinicia.WebApi.Controllers.V1
{
    [Authorize]
    public class AppointmentsController : BaseApiController
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public AppointmentsController(IAppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = await _appointmentService.GetAppointmentsAsync(UserId, page, pageSize);

            return Success(_mapper.Map<PagedResult<AppointmentResult>>(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var result = await _appointmentService.GetAppointmentAsync(id.ParseGuid());

            return Success(_mapper.Map<AppointmentResult>(result));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatedAppointmentModel model)
        {
            var result = await _appointmentService.AddAppointmentAsync(UserId, _mapper.Map<CreatedAppointment>(model));

            return Success(result);
        }
    }
}
