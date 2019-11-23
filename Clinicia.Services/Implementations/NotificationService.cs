using System;
using System.Threading.Tasks;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;

namespace Clinicia.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PagedResult<Notification>> GetNotificationsAsync(int page, int pageSize, Guid userId)
        {
            return _unitOfWork.NotificationRepository.GetNotificationsAsync(page, pageSize, userId);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.NotificationRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public void MarkAsRead(Guid id)
        {
            var notification = _unitOfWork.NotificationRepository.Get(id);
            notification.HasRead = true;

            _unitOfWork.Complete();
        }

        public int GetUnreadNotificationCount(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void ResetUnreadNotificationCount(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
