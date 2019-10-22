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
        Task<PagedResult<Doctor>> GetDoctorsAsync(int page, int pageSize, FilterDoctor filter,
            SortOptions<SortDoctorField> sortOptions);

        Task<DoctorDetails> GetAsync(Guid id);

        Task<DoctorWorkingTime> GetAvailableWorkingTimeAsync(Guid id, DateTime date);

        Task<PagedResult<DoctorReview>> GetDoctorReviewsAsync(Guid id, int page, int pageSize);
    }
}
