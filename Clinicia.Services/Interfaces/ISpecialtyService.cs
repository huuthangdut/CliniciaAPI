using System.Threading.Tasks;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;

namespace Clinicia.Services.Interfaces
{
    public interface ISpecialtyService
    {
        Task<PagedResult<Specialty>> GetSpecialtiesAsync(int page, int pageSize);
    }
}