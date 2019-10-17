using Microsoft.AspNetCore.Identity;

namespace Clinicia.Common.Helpers
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
        {
            return base.DefaultError();
        }
    }
}
