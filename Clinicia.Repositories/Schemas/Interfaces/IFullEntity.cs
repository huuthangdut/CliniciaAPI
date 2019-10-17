namespace Clinicia.Repositories.Schemas.Interfaces
{
    public interface IFullEntity : IEntity, IActiveableEntity, ISoftDeleteEntity, IAuditableEntity
    {
    }
}