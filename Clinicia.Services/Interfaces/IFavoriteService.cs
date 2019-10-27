using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using System;
using System.Threading.Tasks;

namespace Clinicia.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task<PagedResult<UserFavorite>> GetUserFavoritesAsync(Guid userId, int page, int pageSize);

        Task AddToFavorite(Guid userId, Guid doctorId);

        Task RemoveFromFavorite(Guid userId, Guid doctorId);
    }
}
