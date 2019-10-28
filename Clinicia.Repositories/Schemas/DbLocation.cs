using System;
using Clinicia.Repositories.Schemas.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinicia.Repositories.Schemas
{
    [Table("Locations")]
    public class DbLocation : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string FormattedAddress { get; set; }
    }
}