using Clinicia.Common.Enums;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Input;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;
using System.Threading.Tasks;

namespace Clinicia.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<Doctor>> GetDoctorsAsync(int page, int pageSize, FilterDoctor filter, SortOptions<SortDoctorField> sortOptions)
        {
            return await _unitOfWork.DoctorRepository.GetDoctorsAsync(page, pageSize, filter, sortOptions);
        }
    }
}
