using Clinicia.Common.Enums;
using Clinicia.Common.Exceptions;
using Clinicia.Common.Runtime.Security;
using Clinicia.Repositories.Schemas;
using Clinicia.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Clinicia.Dtos.Input;

namespace Clinicia.Services.Implementations
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<DbPatient> _userManager;

        private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;

        public RegisterService(UserManager<DbPatient> userManager, ITwoFactorAuthenticationService twoFactorAuthenticationService)
        {
            _userManager = userManager;
            _twoFactorAuthenticationService = twoFactorAuthenticationService;
        }

        public async Task<string> RegisterAccountAsync(AccountRegister accountRegister)
        {
            var user = new DbPatient
            {
                FirstName = accountRegister.FirstName,
                LastName = accountRegister.LastName,
                UserName = accountRegister.PhoneNumber,
                Email = accountRegister.Email,
                PhoneNumber = accountRegister.PhoneNumber,
                PhoneNumberConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, accountRegister.Password);
            if(!result.Succeeded)
            {
                throw new BusinessException(ErrorCodes.IdentityError.ToString(), result.Errors?.FirstOrDefault().Description);
            }

            await _userManager.AddToRoleAsync(user, IdentityRoles.Patient);

            var verifiedToken = await _twoFactorAuthenticationService.RequestVerifiedTokenAsync(user.PhoneNumber);

            return verifiedToken;
        }
    }
}
