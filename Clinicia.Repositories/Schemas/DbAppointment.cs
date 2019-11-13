using Clinicia.Repositories.Schemas.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinicia.Repositories.Schemas
{
    [Table("Appointments")]
    public class DbAppointment : IFullEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int TotalMinutes { get; set; }

        public decimal TotalPrice { get; set; }

        public string Note { get; set; }

        public string PublicResult { get; set; }

        public string PrivateResult { get; set; }

        public int Status { get; set; }

        public Guid CheckingServiceId { get; set; }

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

        [ForeignKey("CheckingServiceId")]
        public virtual DbCheckingService CheckingService { get; set; }

        [ForeignKey("DoctorId")]
        public virtual DbDoctor Doctor { get; set; }

        [ForeignKey("PatientId")]
        public virtual DbPatient Patient { get; set; }
    }
}