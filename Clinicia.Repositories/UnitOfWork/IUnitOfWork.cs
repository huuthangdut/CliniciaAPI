using System.Threading.Tasks;

namespace Clinicia.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        CliniciaDbContext Context { get; }

        int Complete();

        Task<int> CompleteAsync();
    }
}