using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Clinicia.Repositories.Audits
{
    public interface IAuditHelper
    {
        void ApplyAuditConcepts(EntityEntry entry);
    }
}