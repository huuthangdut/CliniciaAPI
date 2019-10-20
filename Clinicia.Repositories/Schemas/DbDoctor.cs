using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Clinicia.Repositories.Schemas.Interfaces;

namespace Clinicia.Repositories.Schemas
{
    [Table("Doctors")]
    public class DbDoctor : DbUser
    {
        public decimal? Price { get; set; }

        [StringLength(256)]
        public string Clinic { get; set; }

        [StringLength(256)]
        public string MedicalSchool { get; set; }

        public string Awards { get; set; }

        public int? YearExperience { get; set; }

        public Guid? SpecialtyId { get; set; }

        [ForeignKey("SpecialtyId")]
        public virtual DbSpecialty Specialty { get; set; }

        public virtual ICollection<DbAppointment> Appointments { get; set; }

        public virtual ICollection<DbWorkingSchedule> WorkingSchedules { get; set; }

        public virtual ICollection<DbNoAttendance> NoAttendances { get; set; }

        public virtual ICollection<DbReview> Reviews { get; set; }
    }
}