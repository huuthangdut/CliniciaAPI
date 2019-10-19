using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinicia.Repositories.Schemas
{
    [Table("Doctors")]
    public class DbDoctor : DbUser
    {
        [StringLength(256)]
        public string Clinic { get; set; }

        [StringLength(256)]
        public string MedicalSchool { get; set; }

        public string Awards { get; set; }

        public int? YearExperience { get; set; }

        public int? SpecialtyId { get; set; }

        public int? LocationId { get; set; }

        [ForeignKey("SpecialtyId")]
        public virtual DbSpecialty Specialty { get; set; }

        [ForeignKey("LocationId")]
        public virtual DbLocation Location { get; set; }

        public virtual ICollection<DbAppointment> Appointments { get; set; }

        public virtual ICollection<DbWorkingSchedule> WorkingSchedules { get; set; }

        public virtual ICollection<DbNoAttendance> NoAttendances { get; set; }

        public virtual ICollection<DbReview> Reviews { get; set; }
    }
}