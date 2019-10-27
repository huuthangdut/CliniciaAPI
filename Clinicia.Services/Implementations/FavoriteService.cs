using Clinicia.Common.Exceptions;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;
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

        public async Task AddToFavorite(Guid userId, Guid doctorId)
        {
            var dbFavorite = await _unitOfWork.FavoriteRepository.GetFirstOrDefaultAsync(x => x.PatientId == userId && x.DoctorId == doctorId);

            if (dbFavorite == null)
            {
                await _unitOfWork.FavoriteRepository.AddAsync(new DbFavorite
                {
                    PatientId = userId,
                    DoctorId = doctorId
                });
            }
            else if (!dbFavorite.IsActive)
            {
                dbFavorite.IsActive = true;
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task RemoveFromFavorite(Guid userId, Guid doctorId)
        {
            var dbFavorite = await _unitOfWork.FavoriteRepository.GetFirstOrDefaultAsync(x => x.PatientId == userId && x.DoctorId == doctorId && x.IsActive);

            if (dbFavorite == null)
            {
                throw new EntityNotFoundException();
            }

            dbFavorite.IsActive = false;

            await _unitOfWork.CompleteAsync();
        }
    }
}
