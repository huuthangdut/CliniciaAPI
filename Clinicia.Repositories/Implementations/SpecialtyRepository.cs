using AutoMapper;
using Clinicia.Repositories.Helpers.Linq;
using Clinicia.Repositories.Interfaces;
using Clinicia.Repositories.Schemas;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;

namespace Clinicia.Repositories.Implementations
{
    public class SpecialtyRepository : GenericRepository<DbSpecialty>, ISpecialtyRepository
    {
        private readonly IMapper _mapper;

        public SpecialtyRepository(CliniciaDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedResult<Specialty>> GetSpecialtiesAsync(int page, int pageSize, bool? isActive)
        {
            return await Context.Specialties
                .Include(x => x.Doctors)
                .WhereIf(isActive.HasValue, x => x.IsActive == isActive)
                .OrderBy(x => x.Name)
                .GetPagedResultAsync(
                    page,
                    pageSize,
                    x => _mapper.Map<Specialty>(x)
                );
        }
    }
}