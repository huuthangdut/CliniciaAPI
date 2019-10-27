using AutoMapper;
using Clinicia.Common.Helpers;
using Clinicia.Dtos.Common;
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
    public class FavoritesController : BaseApiController
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IMapper _mapper;

        public FavoritesController(
            IFavoriteService favoriteService,
            IMapper mapper)
        {
            _favoriteService = favoriteService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page, [FromQuery]int pageSize)
        {
            var result = await _favoriteService.GetUserFavoritesAsync(UserId, page, pageSize);

            return Success(_mapper.Map<PagedResult<UserFavoriteResult>>(result));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FavoriteModel model)
        {
            await _favoriteService.AddToFavorite(UserId, model.DoctorId.ParseGuid());

            return Success();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] FavoriteModel model)
        {
            await _favoriteService.RemoveFromFavorite(UserId, model.DoctorId.ParseGuid());

            return Success();
        }
    }
}