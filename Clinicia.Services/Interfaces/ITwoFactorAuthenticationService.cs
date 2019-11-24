using Clinicia.Dtos.Output;
using System.Threading.Tasks;

namespace Clinicia.Services.Interfaces
{
    public interface ITwoFactorAuthenticationService
    {
        Task<string> RequestVerifiedTokenAsync(string phoneNumber);

        Task<UserLoginInfo> VerifyAccountAsync(string otpCode, string token);
    }
}
