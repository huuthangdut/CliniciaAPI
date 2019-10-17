using AutoMapper;
using Clinicia.Entities.Common;
using Clinicia.Infrastructure.ApiControllers;
using Clinicia.Services.Interfaces;
using Clinicia.WebApi.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clinicia.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class SpecialtiesController : BaseApiController
    {
        private readonly ISpecialtyService specialtyService;

        private readonly IMapper mapper;

        public SpecialtiesController(ISpecialtyService specialtyService, IMapper mapper)
        {
            this.specialtyService = specialtyService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery]int page, 
            [FromQuery]int pageSize)
        {
            var result = await specialtyService.GetSpecialtiesAsync(page, pageSize);

            return Success(mapper.Map<PagedResult<SpecialtyResult>>(result));
        }
    }
}
