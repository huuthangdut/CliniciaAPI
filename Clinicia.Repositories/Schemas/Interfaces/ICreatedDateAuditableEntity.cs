using System;

namespace Clinicia.Repositories.Schemas.Interfaces
{
    public interface ICreatedDateAuditableEntity
    {
        DateTime? CreatedDate { get; set; }
    }
}