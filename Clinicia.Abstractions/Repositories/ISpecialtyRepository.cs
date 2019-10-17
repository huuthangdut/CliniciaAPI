namespace Clinicia.Abstractions.Repositories
{
    public interface ISpecialtyRepository<T> : IGenericRepository<T>
        where T : class, IEntity
    {
    }
}