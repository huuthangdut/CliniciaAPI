using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinicia.Repositories.Schemas
{
    [Table("Patients")]
    public class DbPatient : DbUser
    {
        public virtual ICollection<DbAppointment> Appointments { get; set; }

        public virtual ICollection<DbReview> Reviews { get; set; }
    }
}