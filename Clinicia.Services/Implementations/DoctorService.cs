using Clinicia.Common.Enums;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Input;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;
using System;
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

        public async Task<DoctorDetails> GetAsync(Guid id)
        {
            return await _unitOfWork.DoctorRepository.GetDoctorAsync(id);
        }

        public async Task<DoctorWorkingTime> GetAvailableWorkingTimeAsync(Guid id, DateTime date)
        {
            return await _unitOfWork.DoctorRepository.GetAvailableWorkingTimeAsync(id, date);
        }

        public async Task<PagedResult<DoctorReview>> GetDoctorReviewsAsync(Guid id, int page, int pageSize)
        {
            return await _unitOfWork.ReviewRepository.GetDoctorReviewsAsync(id, page, pageSize);
        }

        public DoctorCheckingService[] GetCheckingServices(Guid id)
        {
            return _unitOfWork.DoctorRepository.GetCheckingServices(id);
        }
    }
}
