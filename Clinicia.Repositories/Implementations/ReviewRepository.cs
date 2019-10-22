using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Helpers.Linq;
using Clinicia.Repositories.Interfaces;
using Clinicia.Repositories.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Clinicia.Repositories.Implementations
{
    public class ReviewRepository : GenericRepository<DbReview>, IReviewRepository
    {
        private readonly IMapper _mapper;

        public ReviewRepository(CliniciaDbContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public Task<PagedResult<DoctorReview>> GetDoctorReviewsAsync(Guid doctorId, int page, int pageSize)
        {
            return Context.Reviews
                .Include(x => x.Patient)
                .Where(x => x.IsActive && x.DoctorId == doctorId)
                .OrderByDescending(x => x.ReviewDate)
                .GetPagedResultAsync(page, pageSize, x => _mapper.Map<DoctorReview>(x));
        }
    }
}
