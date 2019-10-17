using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Clinicia.Repositories.Schemas
{
    public class DbRole : IdentityRole<string>
    {
        public virtual ICollection<DbUserRole> UserRoles { get; set; }
    }
}