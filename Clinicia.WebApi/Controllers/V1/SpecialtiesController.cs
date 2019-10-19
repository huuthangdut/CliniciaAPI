using AutoMapper;
using Clinicia.Infrastructure.ApiControllers;
using Clinicia.Services.Interfaces;
using Clinicia.WebApi.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Clinicia.Dtos.Common;

namespace Clinicia.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class SpecialtiesController : BaseApiController
    {
        private readonly ISpecialtyService _specialtyService;

        private readonly IMapper _mapper;

        public SpecialtiesController(ISpecialtyService specialtyService, IMapper mapper)
        {
            _specialtyService = specialtyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery]int page, 
            [FromQuery]int pageSize)
        {
            var result = await _specialtyService.GetSpecialtiesAsync(page, pageSize);

            return Success(_mapper.Map<PagedResult<SpecialtyResult>>(result));
        }
    }
}
