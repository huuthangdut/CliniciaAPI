using System.Threading.Tasks;
using Clinicia.Dtos.Output;

namespace Clinicia.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResult> LoginMobileAsync(string username, string password, bool isUserLogin);

        Task<LoginResult> LoginMobileWithTwoFactorAsync(string code, string token);
    }
}