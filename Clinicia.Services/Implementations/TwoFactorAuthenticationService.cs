using AutoMapper;
using Clinicia.Common.Enums;
using Clinicia.Common.Exceptions;
using Clinicia.Common.Helpers;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Clinicia.Services.Implementations
{
    public class TwoFactorAuthenticationService : ITwoFactorAuthenticationService
    {
        private readonly UserManager<DbUser> _userManager;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ISmsService _smsService;

        private readonly IMapper _mapper;

        public TwoFactorAuthenticationService(
            UserManager<DbUser> userManager,
            IUnitOfWork unitOfWork,
            ISmsService smsService,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _smsService = smsService;
            _mapper = mapper;
        }

        public async Task<string> RequestVerifiedTokenAsync(string phoneNumber)
        {
            var user = await _userManager.FindByNameAsync(phoneNumber);
            if(user == null || !user.IsActive)
            {
                throw new BusinessException(ErrorCodes.Failed.ToString(), "Không tìm thấy người dùng.");
            }

            var twoFactorAuthOtp = await GenerateTwoFactorAuthenticationOtp(user);

            await _smsService.SendAsync($"Mã xác thực của bạn là: {twoFactorAuthOtp.Code}", user.UserName);

            return twoFactorAuthOtp.Token;
        }

        public async Task<Dtos.Output.UserLoginInfo> VerifyAccountAsync(string otpCode, string token)
        {
            var user = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.OtpCode == otpCode && x.OtpToken == token && x.OtpExpiredAt > DateTime.UtcNow, x => x.Location);
            if(user == null)
            {
                throw new BusinessException(ErrorCodes.Failed.ToString(), "Mã xác thực không hợp lệ.");
            }

            user.OtpToken = null;
            user.OtpCode = null;
            user.OtpExpiredAt = null;
            user.PhoneNumberConfirmed = true;

            await _unitOfWork.CompleteAsync();

            var roles = await _userManager.GetRolesAsync(user);

            var userInfo = _mapper.Map<Dtos.Output.UserLoginInfo>(user);
            userInfo.Roles = roles.Join(",");
            userInfo.Latitude = user.Location?.Latitude ?? 0;
            userInfo.Longitude = user.Location?.Longitude ?? 0;
            userInfo.Address = user.Location?.FormattedAddress ?? "";

            return userInfo;
        }

        private async Task<(string Code, string Token)> GenerateTwoFactorAuthenticationOtp(DbUser user)
        {
            var otpCode = RandomHelper.GetRandomNumber(6);
            var otpToken = RandomHelper.GetRandomString(128);
            user.OtpCode = otpCode;
            user.OtpToken = otpToken;
            user.OtpExpiredAt = DateTime.UtcNow.AddMinutes(5);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return (Code: otpCode, Token: otpToken);
            }

            throw new BusinessException(ErrorCodes.Failed.ToString(), "Cannot generate two factor authentication otp");
        }
    }
}
