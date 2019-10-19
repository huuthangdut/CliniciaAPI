using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;
using System.Threading.Tasks;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;

namespace Clinicia.Services.Implementations
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpecialtyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<Specialty>> GetSpecialtiesAsync(int page, int pageSize)
        {
            return await _unitOfWork.SpecialtyRepository.GetSpecialtiesAsync(page, pageSize, isActive: true);
        }
    }
}
