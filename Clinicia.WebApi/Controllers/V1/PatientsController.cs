using System.Threading.Tasks;
using AutoMapper;
using Clinicia.Dtos.Input;
using Clinicia.Infrastructure.ApiControllers;
using Clinicia.Services.Interfaces;
using Clinicia.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinicia.WebApi.Controllers.V1
{
    public class PatientsController : BaseApiController
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public PatientsController(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpPost("location")]
        public async Task<IActionResult> Location([FromBody] UserLocationModel model)
        {
            await _patientService.SetLocationAsync(UserId, _mapper.Map<UserLocation>(model));

            return Success();
        }

    }
}
