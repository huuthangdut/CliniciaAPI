using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Helpers.Linq;
using Clinicia.Repositories.Interfaces;
using Clinicia.Repositories.Projections;
using Clinicia.Repositories.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Clinicia.Repositories.Implementations
{
    public class FavoriteRepository : GenericRepository<DbFavorite>, IFavoriteRepository
    {
        private readonly IMapper _mapper;

        public FavoriteRepository(CliniciaDbContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public Task<PagedResult<UserFavorite>> GetUserFavoritesAsync(Guid id, int page, int pageSize)
        {
            return Context.Favorites
                .Include(x => x.Doctor)
                .ThenInclude(d => d.Reviews)
                .Where(x => x.IsActive)
                .Select(x => new UserFavoriteProjection
                {
                    Id = x.Id,
                    Doctor = new FavoriteDoctorProjection
                    {
                        Id = x.Doctor.Id,
                        Name = x.Doctor.FirstName + " " + x.Doctor.LastName,
                        ImageProfile = x.Doctor.ImageProfile,
                        Rating = (decimal?)x.Doctor.Reviews.Where(r => r.IsActive).Sum(r => r.Rating) / x.Doctor.Reviews.Count(r => r.IsActive),
                    }
                })
                .OrderBy(x => x.Doctor.Name)
                .GetPagedResultAsync(page, pageSize, x => _mapper.Map<UserFavorite>(x));
        }
    }
}
