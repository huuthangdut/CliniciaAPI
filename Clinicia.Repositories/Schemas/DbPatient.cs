using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinicia.Repositories.Schemas
{
    [Table("Patients")]
    public class DbPatient : DbUser
    {
        public bool PushNotificationEnabled { get; set; }

        public int UnseenNotificationCount { get; set; }

        public virtual ICollection<DbAppointment> Appointments { get; set; }

        public virtual ICollection<DbReview> Reviews { get; set; }

        public virtual ICollection<DbFavorite> Favorites { get; set; }
    }
}