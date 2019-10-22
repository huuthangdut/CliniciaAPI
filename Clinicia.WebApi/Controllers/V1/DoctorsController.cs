using AutoMapper;
using Clinicia.Common.Enums;
using Clinicia.Common.Extensions;
using Clinicia.Common.Helpers;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Input;
using Clinicia.Infrastructure.ApiControllers;
using Clinicia.Services.Interfaces;
using Clinicia.WebApi.Models;
using Clinicia.WebApi.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Clinicia.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class DoctorsController : BaseApiController
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorsController(IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromQuery] string sort, 
            [FromQuery] FilterDoctorParams filterParams
        )
        {
            var filter = _mapper.Map<FilterDoctor>(filterParams);
            var sortOptions = sort.ToSortOptions<SortDoctorField>();

            var result = await _doctorService.GetDoctorsAsync(page, pageSize, filter, sortOptions);

            return Success(_mapper.Map<PagedResult<DoctorResult>>(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var result = await _doctorService.GetAsync(new Guid(id));

            return Success(_mapper.Map<DoctorDetailsResult>(result));
        }

        [HttpGet("{id}/workingTime")]
        public async Task<IActionResult> GetWorkingTime([FromRoute] string id, [FromQuery] WorkingTimeParams workingTimeParams)
        {
            var result = await _doctorService.GetAvailableWorkingTimeAsync(id.ParseGuid(), workingTimeParams.Date.FromUnixTimeStamp().GetValueOrDefault());

            return Success(_mapper.Map<DoctorWorkingTimeResult>(result));
        }

        [HttpGet("{id}/reviews")]
        public async Task<IActionResult> GetDoctorReviews(
            [FromRoute] string id,
            [FromQuery] int page,
            [FromQuery] int pageSize)
        {
            var result = await _doctorService.GetDoctorReviewsAsync(id.ParseGuid(), page, pageSize);

            return Success(_mapper.Map<PagedResult<DoctorReviewResult>>(result));
        }

    }
}
