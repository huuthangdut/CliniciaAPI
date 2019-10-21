using System;
using System.Threading.Tasks;
using Clinicia.Common.Enums;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Input;
using Clinicia.Dtos.Output;

namespace Clinicia.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<PagedResult<Doctor>> GetDoctorsAsync(int page, int pageSize, FilterDoctor filter, SortOptions<SortDoctorField> sortOptions);

        Task<DoctorDetails> GetDoctorAsync(Guid id);

        Task<DoctorWorkingTime> GetAvailableWorkingTime(Guid id, DateTime date);
    }
}