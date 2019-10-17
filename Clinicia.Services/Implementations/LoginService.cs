using Clinicia.Common;
using Clinicia.Common.Enums;
using Clinicia.Common.Runtime.Security;
using Clinicia.Entities.Login;
using Clinicia.Repositories.Schemas;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
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

        private JwtIssuerOptions _jwtIssuerOptions;

        public LoginService(
            SignInManager<DbUser> signInManager,
            UserManager<DbUser> userManager,
            IOptions<JwtIssuerOptions> jwtIssuerOptions,
            IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtIssuerOptions = jwtIssuerOptions.Value;
        }

        public async Task<LoginResult> LoginMobileAsync(string username, string password)
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

            var roles = await _userManager.GetRolesAsync(user);
            var claimIdentity = GenerateClaimsIdentity(user, roles.Join(","));

            return new LoginResult(claimIdentity);
        }

        private ClaimsIdentity GenerateClaimsIdentity(DbUser user, string role)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimIdentityTypes.UserId, user.Id),
                new Claim(ClaimIdentityTypes.UserName, user.UserName),
                new Claim(ClaimIdentityTypes.FirstName, user.FirstName),
                new Claim(ClaimIdentityTypes.LastName, user.LastName),
                new Claim(ClaimIdentityTypes.Email, user.Email),
                new Claim(ClaimIdentityTypes.PhoneNumber, user.PhoneNumber)
            };

            if(!string.IsNullOrEmpty(user.ImageProfile))
            {
                userClaims.Add(new Claim(ClaimIdentityTypes.ImageProfile, user.ImageProfile));
            }

            if(!string.IsNullOrEmpty(role))
            {
                userClaims.Add(new Claim(ClaimIdentityTypes.Role, role));
            }

            return new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), userClaims);
        }
    }
}
