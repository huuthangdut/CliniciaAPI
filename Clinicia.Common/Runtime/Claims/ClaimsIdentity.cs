using Clinicia.Common.Runtime.Security;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;

namespace Clinicia.Common.Runtime.Claims
{
    public class ClaimsIdentity : IClaimsIdentity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsPrincipal Principal => _httpContextAccessor.HttpContext?.User;

        public ClaimsIdentity(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var userIdClaim = Principal?.Claims.FirstOrDefault(c => c.Type == ClaimIdentityTypes.UserId);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    throw new AuthenticationException("User not login to system");
                }

                int userId;
                if (!int.TryParse(userIdClaim.Value, out userId))
                {
                    throw new AuthenticationException("User not login to system");
                }

                return userId;
            }
        }

        public int? GetUserId()
        {
            var userIdClaim = Principal?.Claims.FirstOrDefault(c => c.Type == ClaimIdentityTypes.UserId);
            if (string.IsNullOrEmpty(userIdClaim?.Value))
            {
                return null;
            }

            int userId;
            if (!int.TryParse(userIdClaim.Value, out userId))
            {
                return null;
            }

            return userId;
        }

        public string UserName => Principal?.Claims.FirstOrDefault(c => c.Type == ClaimIdentityTypes.UserName)?.Value;

        public string[] CurrentUserRoles => Principal?.Claims.Where(c => c.Type == ClaimIdentityTypes.Role).Select(x => x.Value).ToArray();

        public bool CurrentUserIsInRole(string roleName)
        {
            return CurrentUserRoles.Any(x => x == roleName);
        }
    }
}