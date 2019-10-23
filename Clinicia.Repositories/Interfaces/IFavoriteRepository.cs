using System;
using System.Threading.Tasks;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;

namespace Clinicia.Repositories.Interfaces
{
    public interface IFavoriteRepository : IGenericRepository<DbFavorite>
    {
        Task<PagedResult<UserFavorite>> GetUserFavoritesAsync(Guid id, int page, int pageSize);
    }
}