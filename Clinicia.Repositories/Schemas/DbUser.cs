using Clinicia.Repositories.Schemas.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinicia.Repositories.Schemas
{
    public class DbUser : IdentityUser<string>, ISoftDeleteEntity, IActiveableEntity, IAuditableEntity
    {
        [StringLength(256)]
        public string FirstName { get; set; }

        [StringLength(256)]
        public string LastName { get; set; }

        public string ImageProfile { get; set; }

        public bool? Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(50)]
        public string UpdatedUser { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public virtual ICollection<DbUserRole> UserRoles { get; set; }
    }
}