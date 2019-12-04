using AutoMapper;
using Clinicia.Common.Enums;
using Clinicia.Common.Runtime.Security;
using Clinicia.Dtos.Input;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Clinicia.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly UserManager<DbUser> _userManager;

        private readonly ITokenService _tokenService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService, UserManager<DbUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<JwtTokenResult> SetLocationAsync(Guid userId, UserLocation location)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(userId);
            user.Location = new DbLocation
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                FormattedAddress = location.FormattedAddress
            };

            await _unitOfWork.CompleteAsync();

            var roles = await _userManager.GetRolesAsync(user);

            var userInfo = _mapper.Map<Dtos.Output.UserLoginInfo>(user);
            userInfo.Roles = roles.Join(",");
            userInfo.Latitude = user.Location.Latitude;
            userInfo.Longitude = user.Location.Longitude;
            userInfo.Address = user.Location.FormattedAddress;

            var claimIdentity = GenerateClaimsIdentity(userInfo);

            return await _tokenService.RequestTokenAsync(claimIdentity, ApplicationType.Mobile);
        }

        private ClaimsIdentity GenerateClaimsIdentity(Dtos.Output.UserLoginInfo user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimIdentityTypes.UserId, user.Id.ToString()),
                new Claim(ClaimIdentityTypes.UserName, user.UserName),
                new Claim(ClaimIdentityTypes.Email, user.Email),
                new Claim(ClaimIdentityTypes.FirstName, user.FirstName),
                new Claim(ClaimIdentityTypes.LastName, user.LastName),
                new Claim(ClaimIdentityTypes.PhoneNumber, user.PhoneNumber),
                new Claim(ClaimIdentityTypes.Latitude, user.Latitude.ToString()),
                new Claim(ClaimIdentityTypes.Longitude, user.Longitude.ToString()),
                new Claim(ClaimIdentityTypes.Address, user.Address)
            };

            if (!string.IsNullOrEmpty(user.ImageProfile))
            {
                userClaims.Add(new Claim(ClaimIdentityTypes.ImageProfile, user.ImageProfile));
            }

            if (!string.IsNullOrEmpty(user.Roles))
            {
                userClaims.Add(new Claim(ClaimIdentityTypes.Role, user.Roles));
            }

            return new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), userClaims);
        }
    }
}