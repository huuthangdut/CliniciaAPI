using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Clinicia.Services.Implementations
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<UserFavorite>> GetUserFavoritesAsync(Guid userId, int page, int pageSize)
        {
            return await _unitOfWork.FavoriteRepository.GetUserFavoritesAsync(userId, page, pageSize);
        }
    }
}
