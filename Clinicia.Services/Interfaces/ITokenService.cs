using Clinicia.Common.Enums;
using System.Security.Claims;
using System.Threading.Tasks;
using Clinicia.Dtos.Output;

namespace Clinicia.Services.Interfaces
{
    public interface ITokenService
    {
        Task<JwtTokenResult> RequestTokenAsync(ClaimsIdentity identity, ApplicationType application);
    }
}
