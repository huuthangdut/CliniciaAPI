using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Clinicia.Repositories.Schemas.Interfaces;

namespace Clinicia.Repositories.Schemas
{
    [Table("Reviews")]
    public class DbReview : IFullEntity
    {
        [Key]
        public Guid Id { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public Guid DoctorId { get; set; }

        public Guid PatientId { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(50)]
        public string UpdatedUser { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        [ForeignKey("DoctorId")]
        public virtual DbDoctor Doctor { get; set; }

        [ForeignKey("PatientId")]
        public virtual DbPatient Patient { get; set; }
    }
}