using Clinicia.Repositories.Schemas.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinicia.Repositories.Schemas
{
    [Table("Specialties")]
    public class DbSpecialty : IFullEntity
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public string Image { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(50)]
        public string UpdatedUser { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public virtual ICollection<DbDoctor> Doctors { get; set; }
    }
}   