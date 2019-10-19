using Clinicia.Repositories.Schemas;
using System.Threading.Tasks;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;

namespace Clinicia.Repositories.Interfaces
{
    public interface ISpecialtyRepository : IGenericRepository<DbSpecialty>
    {
        Task<PagedResult<Specialty>> GetSpecialtiesAsync(int page, int pageSize, bool? isActive);
    }
}