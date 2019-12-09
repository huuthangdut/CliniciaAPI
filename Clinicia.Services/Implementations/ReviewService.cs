using System;
using System.Threading.Tasks;
using Clinicia.Dtos.Input;
using Clinicia.Repositories.Schemas;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;

namespace Clinicia.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddReview(Guid userId, CreatedReview model)
        {
            await _unitOfWork.ReviewRepository.AddAsync(new DbReview
            {
                Rating = model.Rating,
                Comment = model.Comment,
                ReviewDate = DateTime.UtcNow,
                PatientId = userId,
                DoctorId = model.DoctorId,
                AppointmentId = model.AppointmentId
            });

            await _unitOfWork.CompleteAsync();
        }
    }
}
