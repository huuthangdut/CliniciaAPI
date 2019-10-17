using Clinicia.Common.Extensions;
using Clinicia.Common.Runtime.Claims;
using Clinicia.Repositories.Schemas.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Clinicia.Repositories.Audits
{
    public class AuditHelper : IAuditHelper
    {
        private readonly IClaimsIdentity _claimsIdentity;

        public AuditHelper(IClaimsIdentity claimsIdentity)
        {
            _claimsIdentity = claimsIdentity;
        }

        public void ApplyAuditConcepts(EntityEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyConceptsForAddedEntity(entry);
                    break;
                case EntityState.Modified:
                    ApplyConceptsForModifiedEntity(entry);
                    break;
                case EntityState.Deleted:
                    ApplyConceptsForDeletedEntity(entry);
                    break;
            }
        }

        private void ApplyConceptsForAddedEntity(EntityEntry entry)
        {
            SetCreationStatusForStatusProperty(entry);
            SetCreationAuditProperties(entry.Entity);
        }

        private void ApplyConceptsForModifiedEntity(EntityEntry entry)
        {
            SetModificationAuditProperties(entry.Entity);
        }

        private void ApplyConceptsForDeletedEntity(EntityEntry entry)
        {
            CancelDeletionForSoftDelete(entry);
        }

        private void CancelDeletionForSoftDelete(EntityEntry entry)
        {
            if (!(entry.Entity is ISoftDeleteEntity))
            {
                return;
            }

            entry.Reload();
            entry.State = EntityState.Modified;
            entry.Entity.As<ISoftDeleteEntity>().IsDelete = true;
        }

        private static void SetCreationStatusForStatusProperty(EntityEntry entry)
        {
            if (entry.Entity is IActiveableEntity)
            {
                entry.Entity.As<IActiveableEntity>().IsActive = true;
            }

            if (entry.Entity is ISoftDeleteEntity)
            {
                entry.Entity.As<ISoftDeleteEntity>().IsDelete = false;
            }
        }

        private void SetModificationAuditProperties(object entityAsObj)
        {
            if (entityAsObj is IUpdatedDateAuditableEntity)
            {
                entityAsObj.As<IUpdatedDateAuditableEntity>().UpdatedDate = DateTime.Now;
            }

            if (entityAsObj is IUpdatedUserAuditableEntity)
            {
                entityAsObj.As<IUpdatedUserAuditableEntity>().UpdatedUser = _claimsIdentity.UserName;
            }
        }

        private void SetCreationAuditProperties(object entityAsObj)
        {
            if (entityAsObj is ICreatedDateAuditableEntity)
            {
                entityAsObj.As<ICreatedDateAuditableEntity>().CreatedDate = DateTime.Now;
            }

            if (entityAsObj is ICreatedUserAuditableEntity)
            {
                entityAsObj.As<ICreatedUserAuditableEntity>().CreatedUser = _claimsIdentity.UserName;
            }
        }
    }
}