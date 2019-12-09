using Clinicia.Repositories.Schemas.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clinicia.Repositories.Schemas
{
    [Table("Notifications")]
    public class DbNotification : IFullEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string Image { get; set; }

        public DateTime NotificationDate { get; set; }

        public bool HasRead { get; set; }

        public Guid? UserId { get; set; }

        public Guid? AppointmentId { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(50)]
        public string UpdatedUser { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        [ForeignKey("UserId")]
        public virtual DbUser User { get; set; }
    }
}
