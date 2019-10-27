using Clinicia.Dtos.Input;
using System;
using System.Threading.Tasks;

namespace Clinicia.Services.Interfaces
{
    public interface IReviewService
    {
        Task AddReview(Guid userId, CreatedReview model);
    }
}
