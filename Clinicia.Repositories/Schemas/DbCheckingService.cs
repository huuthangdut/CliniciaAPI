using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Clinicia.Repositories.Schemas.Interfaces;

namespace Clinicia.Repositories.Schemas
{
    [Table("CheckingServices")]
    public class DbCheckingService : IFullEntity
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int DurationInMinutes { get; set; }

        public decimal Price { get; set; }

        public Guid DoctorId { get; set; }

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
    }
}