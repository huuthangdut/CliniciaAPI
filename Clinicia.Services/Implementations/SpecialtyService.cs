using Clinicia.Entities.Common;
using Clinicia.Entities.Specialty;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;
using System.Threading.Tasks;

namespace Clinicia.Services.Implementations
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly IUnitOfWork unitOfWork;

        public SpecialtyService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<Specialty>> GetSpecialtiesAsync(int page, int pageSize)
        {
            return await unitOfWork.SpecialtyRepository.GetSpecialtiesAsync(page, pageSize, isActive: true);
        }
    }
}
