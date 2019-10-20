using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Clinicia.Repositories.Schemas
{
    public class DbRole : IdentityRole<Guid>
    {
        public virtual ICollection<DbUserRole> UserRoles { get; set; }
    }
}