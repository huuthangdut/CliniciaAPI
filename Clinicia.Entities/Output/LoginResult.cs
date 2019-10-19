using System.Security.Claims;
using Clinicia.Common.Enums;

namespace Clinicia.Dtos.Output
{
    public class LoginResult
    {
        public LoginResultType Result { get; }

        public ClaimsIdentity Identity { get; }

        public LoginResult(LoginResultType result)
        {
            Result = result;
        }

        public LoginResult(ClaimsIdentity identity)
            : this(LoginResultType.Success)
        {
            Identity = identity;
        }
    }
}