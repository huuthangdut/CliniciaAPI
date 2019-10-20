using AutoMapper;
using Clinicia.Common.Enums;
using Clinicia.Common.Extensions;
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
    [ApiVersion("1.0")]
    [Authorize]
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

    }
}
