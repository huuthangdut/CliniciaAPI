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
    public class CheckingServicesController : BaseApiController
    {
        private readonly ICheckingService _checkingService;
        private readonly IMapper _mapper;

        public CheckingServicesController(ICheckingService checkingService, IMapper mapper)
        {
            _checkingService = checkingService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _checkingService.GetCheckingServices(UserId);

            return Success(_mapper.Map<DoctorCheckingServiceResult[]>(result));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatedCheckingServiceModel model)
        {
            await _checkingService.AddCheckingServiceAsync(_mapper.Map<CreatedCheckingService>(model));

            return Success();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdatedCheckingServiceModel model)
        {
            await _checkingService.UpdateCheckingServiceAsync(_mapper.Map<UpdatedCheckingService>(model));

            return Success();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _checkingService.DeleteCheckingServiceAsync(id.ParseGuid());

            return Success();
        }
    }
}