using Clinicia.Entities.Common;
using Clinicia.Entities.Specialty;
using System.Threading.Tasks;

namespace Clinicia.Services.Interfaces
{
    public interface ISpecialtyService
    {
        Task<PagedResult<Specialty>> GetSpecialtiesAsync(int page, int pageSize);
    }
}