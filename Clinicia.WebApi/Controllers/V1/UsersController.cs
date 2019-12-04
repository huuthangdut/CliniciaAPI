using System.Threading.Tasks;
using AutoMapper;
using Clinicia.Dtos.Input;
using Clinicia.Infrastructure.ApiControllers;
using Clinicia.Services.Interfaces;
using Clinicia.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinicia.WebApi.Controllers.V1
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("location")]
        public async Task<IActionResult> Location([FromBody] UserLocationModel model)
        {
            var jwtToken = await _userService.SetLocationAsync(UserId, _mapper.Map<UserLocation>(model));

            return Success(jwtToken);
        }

    }
}
