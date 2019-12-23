using Clinicia.Common.Enums;
using Clinicia.Common.Exceptions;
using Clinicia.Common.Runtime.Security;
using Clinicia.Repositories.Schemas;
using Clinicia.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Clinicia.Dtos.Input;
using System;
using System.Collections.Generic;

namespace Clinicia.Services.Implementations
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<DbPatient> _userManager;

        private readonly UserManager<DbDoctor> _doctorManager;

        private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;

        public RegisterService(
            UserManager<DbPatient> userManager,
            UserManager<DbDoctor> doctorManager,
            ITwoFactorAuthenticationService twoFactorAuthenticationService)
        {
            _userManager = userManager;
            _doctorManager = doctorManager;
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

        public async Task<string> RegisterDoctorAsync(AccountDoctorRegister accountDoctorRegister)
        {
            var user = new DbDoctor
            {
                FirstName = accountDoctorRegister.FirstName,
                LastName = accountDoctorRegister.LastName,
                Clinic = accountDoctorRegister.Clinic,
                UserName = accountDoctorRegister.PhoneNumber,
                Email = accountDoctorRegister.Email,
                PhoneNumber = accountDoctorRegister.PhoneNumber,
                PhoneNumberConfirmed = false,
                WorkingSchedules = new List<DbWorkingSchedule>
                {
                    new DbWorkingSchedule { FromDate = DateTime.Now, Hours = "0,0,0,0,0,0,0" }
                }
            };

            var result = await _doctorManager.CreateAsync(user, accountDoctorRegister.Password);
            if (!result.Succeeded)
            {
                throw new BusinessException(ErrorCodes.IdentityError.ToString(), result.Errors?.FirstOrDefault().Description);
            }

            await _doctorManager.AddToRoleAsync(user, IdentityRoles.Doctor);

            var verifiedToken = await _twoFactorAuthenticationService.RequestVerifiedTokenAsync(user.PhoneNumber);

            return verifiedToken;
        }
    }
}
