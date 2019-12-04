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

        public async Task<PagedResult<Doctor>> GetDoctorsAsync(Guid userId, int page, int pageSize, FilterDoctor filter, SortOptions<SortDoctorField> sortOptions)
        {
            return await _unitOfWork.DoctorRepository.GetDoctorsAsync(userId, page, pageSize, filter, sortOptions);
        }

        public async Task<DoctorDetails> GetAsync(Guid userId, Guid doctorId)
        {
            return await _unitOfWork.DoctorRepository.GetDoctorAsync(userId, doctorId);
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
