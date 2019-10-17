using Clinicia.Entities.Login;
using System.Threading.Tasks;

namespace Clinicia.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResult> LoginMobileAsync(string username, string password);
    }
}