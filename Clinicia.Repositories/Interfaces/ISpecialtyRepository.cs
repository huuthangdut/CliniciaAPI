using Clinicia.Entities.Common;
using Clinicia.Entities.Specialty;
using Clinicia.Repositories.Schemas;
using System.Threading.Tasks;

namespace Clinicia.Repositories.Interfaces
{
    public interface ISpecialtyRepository : IGenericRepository<DbSpecialty>
    {
        Task<PagedResult<Specialty>> GetSpecialtiesAsync(int page, int pageSize, bool? isActive);
    }
}