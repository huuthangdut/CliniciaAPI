using AutoMapper;
using Clinicia.Common.Enums;
using Clinicia.Common.Runtime.Security;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Clinicia.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<DbUser> _signInManager;

        private readonly UserManager<DbUser> _userManager;

        private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;

        private readonly IMapper _mapper;

        private JwtIssuerOptions _jwtIssuerOptions;

        private readonly IUnitOfWork _unitOfWork;

        public LoginService(
            SignInManager<DbUser> signInManager,
            UserManager<DbUser> userManager,
            ITwoFactorAuthenticationService twoFactorAuthenticationService,
            IMapper mapper,
            IOptions<JwtIssuerOptions> jwtIssuerOptions,
            IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _twoFactorAuthenticationService = twoFactorAuthenticationService;
            _mapper = mapper;
            _jwtIssuerOptions = jwtIssuerOptions.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResult> LoginMobileAsync(string username, string password, bool isUserLogin)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return new LoginResult(LoginResultType.InvalidUserNameOrPassword);
            }

            if (user.IsActive == false)
            {
                return new LoginResult(LoginResultType.UserIsNotActive);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, true);

            if (!signInResult.Succeeded)
            {
                if (signInResult.IsLockedOut)
                {
                    return new LoginResult(LoginResultType.UserLockedOut);
                }

                return new LoginResult(LoginResultType.InvalidUserNameOrPassword);
            }

            var isDoctorRole = await _userManager.IsInRoleAsync(user, IdentityRoles.Doctor);
            if (!isUserLogin && !isDoctorRole)
            {
                return new LoginResult(LoginResultType.Unauthorized);
            }

            if (!user.PhoneNumberConfirmed)
            {
                return new LoginResult(LoginResultType.RequireConfirmedPhoneNumber);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var dbUser = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.UserName == username, x => x.Location);

            var userInfo = _mapper.Map<Dtos.Output.UserLoginInfo>(user);
            userInfo.Roles = roles.Join(",");
            userInfo.Latitude = dbUser?.Location?.Latitude ?? 0;
            userInfo.Longitude = dbUser?.Location?.Longitude ?? 0;
            userInfo.Address = dbUser?.Location?.FormattedAddress ?? "";

            var claimIdentity = GenerateClaimsIdentity(userInfo);

            return new LoginResult(claimIdentity);
        }

        public async Task<LoginResult> LoginMobileWithTwoFactorAsync(string code, string token)
        {
            var userInfo = await _twoFactorAuthenticationService.VerifyAccountAsync(code, token);

            var claimIdentity = GenerateClaimsIdentity(userInfo);

            return new LoginResult(claimIdentity);
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

            if(!string.IsNullOrEmpty(user.ImageProfile))
            {
                userClaims.Add(new Claim(ClaimIdentityTypes.ImageProfile, user.ImageProfile));
            }

            if(!string.IsNullOrEmpty(user.Roles))
            {
                userClaims.Add(new Claim(ClaimIdentityTypes.Role, user.Roles));
            }

            return new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), userClaims);
        }
    }
}
