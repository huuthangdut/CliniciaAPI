using AutoMapper;
using Clinicia.Common.Enums;
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
    public class AccountController : BaseApiController
    {
        private readonly ILoginService _loginService;

        private readonly ITokenService _tokenService;

        private readonly IRegisterService _registerService;

        private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;

        private readonly IMapper _mapper;

        public AccountController(
            ILoginService loginService, 
            ITokenService tokenService, 
            IRegisterService registerService, 
            IMapper mapper,
            ITwoFactorAuthenticationService twoFactorAuthenticationService
            )
        {
            _loginService = loginService;
            _tokenService = tokenService;
            _registerService = registerService;
            _twoFactorAuthenticationService = twoFactorAuthenticationService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginMobile([FromBody] LoginModel model)
        {
            var loginResult = await _loginService.LoginMobileAsync(model.Username, model.Password, model.IsUserLogin);

            switch (loginResult.Result)
            {
                case LoginResultType.InvalidUserNameOrPassword:
                    return BadRequest(ErrorCodes.UserLoginInvalidUserNameOrPassword, "Tên đăng nhập hoặc mật khẩu không hợp lệ");

                case LoginResultType.UserIsNotActive:
                    return BadRequest(ErrorCodes.UserLoginIsNotActive, "Tài khoản chưa được kích hoạt");

                case LoginResultType.UserLockedOut:
                    return BadRequest(ErrorCodes.UserLockedOut, "Tại khoản đã bị khoá.");

                case LoginResultType.RequireConfirmedPhoneNumber:
                    return BadRequest(ErrorCodes.RequireConfirmedPhoneNumber, "Số điện thoại chưa được xác thực.");

                case LoginResultType.Unauthorized:
                    return BadRequest(ErrorCodes.Unauthorized, "Tài khoản không đủ quyền để đăng nhập.");

                case LoginResultType.Success:
                    var jwtToken = await _tokenService.RequestTokenAsync(loginResult.Identity, ApplicationType.Mobile);
                    return Success(jwtToken);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [HttpPost("request2fa")]
        public async Task<IActionResult> RequestTwoFactor([FromBody] RequestTwoFactorModel model)
        {
            var verifiedToken = await _twoFactorAuthenticationService.RequestVerifiedTokenAsync(model.PhoneNumber);

            return Success(new TokenResult { Token = verifiedToken });
        }

        [HttpPost("verify2fa")]
        public async Task<IActionResult> LoginWithTwoFactorAuthentication([FromBody] VerifiedTwoFactorModel model)
        {
            var loginResult = await _loginService.LoginMobileWithTwoFactorAsync(model.Code, model.Token);

            var jwtToken = await _tokenService.RequestTokenAsync(loginResult.Identity, ApplicationType.Mobile);

            return Success(jwtToken);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var veirfiedToken = await _registerService.RegisterAccountAsync(_mapper.Map<AccountRegister>(model));

            return Success(new TokenResult { Token = veirfiedToken });
        }

        [HttpPost("registerDoctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] RegisterDoctorModel model)
        {
            var veirfiedToken = await _registerService.RegisterDoctorAsync(_mapper.Map<AccountDoctorRegister>(model));

            return Success(new TokenResult { Token = veirfiedToken });
        }
    }
}