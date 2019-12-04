using System;
using System.Threading.Tasks;
using Clinicia.Common.Enums;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Input;
using Clinicia.Dtos.Output;

namespace Clinicia.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<PagedResult<Doctor>> GetDoctorsAsync(Guid userId, int page, int pageSize, FilterDoctor filter,
            SortOptions<SortDoctorField> sortOptions);

        Task<DoctorDetails> GetAsync(Guid userId, Guid doctorId);

        Task<DoctorWorkingTime> GetAvailableWorkingTimeAsync(Guid id, DateTime date);

        Task<PagedResult<DoctorReview>> GetDoctorReviewsAsync(Guid id, int page, int pageSize);

        DoctorCheckingService[] GetCheckingServices(Guid id);
    }
}
