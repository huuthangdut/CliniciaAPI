using Clinicia.Entities.Login;
using System.Threading.Tasks;

namespace Clinicia.Abstractions.Services
{
    public interface ILoginService
    {
        Task<LoginResult> LoginMobileAsync(string username, string password);
    }
}