using Clinicia.Common.Enums;
using Clinicia.Entities.Token;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clinicia.Abstractions.Services
{
    public interface ITokenService
    {
        Task<JwtTokenResult> RequestTokenAsync(ClaimsIdentity identity, ApplicationType application);
    }
}
