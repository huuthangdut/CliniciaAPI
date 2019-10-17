using Clinicia.Common.Enums;
using Clinicia.Common.Exceptions;
using Clinicia.Common.Runtime.Security;
using Clinicia.Entities.Register;
using Clinicia.Repositories.Schemas;
using Clinicia.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Clinicia.Services.Implementations
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<DbPatient> _userManager;

        public RegisterService(UserManager<DbPatient> userManager)
        {
            _userManager = userManager;
        }

        public async Task RegisterAccountAsync(AccountRegister accountRegister)
        {
            var user = new DbPatient
            {
                FirstName = accountRegister.FirstName,
                LastName = accountRegister.LastName,
                Email = accountRegister.Email,
                UserName = accountRegister.Email,
                PhoneNumber = accountRegister.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, accountRegister.Password);
            if(!result.Succeeded)
            {
                throw new BusinessException(ErrorCodes.IdentityError.ToString(), result.Errors?.FirstOrDefault().Description);
            }

            await _userManager.AddToRoleAsync(user, IdentityRoles.Patient);
        }
    }
}
