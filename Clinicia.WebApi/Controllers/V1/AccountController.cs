using AutoMapper;
using Clinicia.Common.Enums;
using Clinicia.Infrastructure.ApiControllers;
using Clinicia.Services.Interfaces;
using Clinicia.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Clinicia.Dtos.Input;

namespace Clinicia.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly IRegisterService _registerService;
        private readonly IMapper _mapper;

        public AccountController(ILoginService loginService, ITokenService tokenService, IRegisterService registerService, IMapper mapper)
        {
            _loginService = loginService;
            _tokenService = tokenService;
            _registerService = registerService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginMobile([FromBody] LoginModel model)
        {
            var loginResult = await _loginService.LoginMobileAsync(model.Username, model.Password);

            switch (loginResult.Result)
            {
                case LoginResultType.InvalidUserNameOrPassword:
                    return BadRequest(ErrorCodes.UserLoginInvalidUserNameOrPassword, "Invalid username or password");

                case LoginResultType.UserIsNotActive:
                    return BadRequest(ErrorCodes.UserLoginIsNotActive, "User login is not active");

                case LoginResultType.UserLockedOut:
                    return BadRequest(ErrorCodes.UserLockedOut, "User locked out");

                case LoginResultType.Success:
                    var jwtToken = await _tokenService.RequestTokenAsync(loginResult.Identity, ApplicationType.Mobile);
                    return Success(jwtToken);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            await _registerService.RegisterAccountAsync(_mapper.Map<AccountRegister>(model));
            return Success();
        }
    }
}