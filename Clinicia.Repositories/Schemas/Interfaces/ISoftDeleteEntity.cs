namespace Clinicia.Repositories.Schemas.Interfaces
{
    public interface ISoftDeleteEntity
    {
        bool IsDelete { get; set; }
    }
}