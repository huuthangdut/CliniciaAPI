using Clinicia.Repositories.Schemas.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinicia.Repositories.Schemas
{
    [Table("WorkingSchedules")]
    public class DbWorkingSchedule : IFullEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime FromDate { get; set; }

        [StringLength(126)]
        public string Hours { get; set; }

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
