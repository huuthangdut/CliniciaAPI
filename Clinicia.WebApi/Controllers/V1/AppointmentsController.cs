using AutoMapper;
using Clinicia.Common.Helpers;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Input;
using Clinicia.Infrastructure.ApiControllers;
using Clinicia.Services.Interfaces;
using Clinicia.WebApi.Models;
using Clinicia.WebApi.Results;
using System.Threading.Tasks;
using Clinicia.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinicia.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class AppointmentsController : BaseApiController
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorAppointmentService _doctorAppointmentService;
        private readonly IMapper _mapper;

        public AppointmentsController(IAppointmentService appointmentService, IDoctorAppointmentService doctorAppointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _doctorAppointmentService = doctorAppointmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int page, 
            [FromQuery] int pageSize,
            [FromQuery] AppointmentStatus[] status
            )
        {
            var result = await _appointmentService.GetAppointmentsAsync(UserId, page, pageSize, status);

            return Success(_mapper.Map<PagedResult<AppointmentResult>>(result));
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcoming()
        {
            var result = await _appointmentService.GetUpcomingAppointment(UserId);

            return Success(_mapper.Map<AppointmentResult>(result));
        }

        [HttpGet("ofdoctor")]
        public async Task<IActionResult> GetOfDoctor(
           [FromQuery] int page,
           [FromQuery] int pageSize,
           [FromQuery] AppointmentStatus[] status
           )
        {
            var result = await _doctorAppointmentService.GetDoctorAppointmentsAsync(UserId, page, pageSize, status);

            return Success(_mapper.Map<PagedResult<DoctorAppointmentResult>>(result));
        }

        [HttpGet("ofdoctor/{id}")]
        public async Task<IActionResult> GetOfDoctor([FromRoute] string id)
        {
            var result = await _doctorAppointmentService.GetDoctorAppointmentAsync(id.ParseGuid());

            return Success(_mapper.Map<DoctorAppointmentResult>(result));
        }

        [HttpPost("ofdoctor/{id}/status")]
        public async Task<IActionResult> Status([FromRoute] string id, [FromBody] StatusModel<int> model)
        {
            await _doctorAppointmentService.UpdateStatus(id.ParseGuid(), (AppointmentStatus)model.Status);

            return Success();
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

        [HttpPost("{id}/cancel")] 
        public async Task<IActionResult> Cancel([FromRoute] string id)
        {
            await _appointmentService.UpdateStatus(id.ParseGuid(), AppointmentStatus.Cancelled);

            return Success();
        }
    }
}
