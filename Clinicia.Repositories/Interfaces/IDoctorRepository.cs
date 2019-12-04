using Clinicia.Common.Enums;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Input;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;
using System;
using System.Threading.Tasks;

namespace Clinicia.Repositories.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<DbDoctor>
    {
        Task<PagedResult<Doctor>> GetDoctorsAsync(Guid userId, int page, int pageSize, FilterDoctor filter, SortOptions<SortDoctorField> sortOptions);

        Task<DoctorDetails> GetDoctorAsync(Guid userId, Guid doctorId);

        Task<DoctorWorkingTime> GetAvailableWorkingTimeAsync(Guid id, DateTime date);

        DoctorCheckingService[] GetCheckingServices(Guid id);
    }
}