using Clinicia.Repositories.Schemas.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinicia.Repositories.Schemas
{
    [Table("DeviceUsers")]
    public class DbDeviceUser : IFullEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string DeviceUuid { get; set; }

        [Required]
        public string DeviceType { get; set; }

        [Required]
        public string DeviceToken { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public DateTime? ExpiredAt { get; set; }

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
