using System;
using System.Threading.Tasks;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;

namespace Clinicia.Repositories.Interfaces
{
    public interface IReviewRepository : IGenericRepository<DbReview>
    {
        Task<PagedResult<DoctorReview>> GetDoctorReviewsAsync(Guid doctorId, int page, int pageSize);
    }
}
