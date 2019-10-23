using Clinicia.Repositories.Schemas.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinicia.Repositories.Schemas
{
    [Table("Favorites")]
    public class DbFavorite : IFullEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }

        public Guid DoctorId { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(50)]
        public string UpdatedUser { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public virtual DbPatient Patient { get; set; }

        public virtual DbDoctor Doctor { get; set; }
    }
}
