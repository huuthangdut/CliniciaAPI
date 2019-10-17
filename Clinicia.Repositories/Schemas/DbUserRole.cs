using Microsoft.AspNetCore.Identity;

namespace Clinicia.Repositories.Schemas
{
    public class DbUserRole : IdentityUserRole<string>
    {
        public virtual DbUser User { get; set; }

        public virtual DbRole Role { get; set; }
    }
}