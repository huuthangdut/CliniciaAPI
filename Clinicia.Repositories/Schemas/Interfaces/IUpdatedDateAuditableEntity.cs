using System;

namespace Clinicia.Repositories.Schemas.Interfaces
{
    public interface IUpdatedDateAuditableEntity
    {
        DateTime? UpdatedDate { get; set; }
    }
}