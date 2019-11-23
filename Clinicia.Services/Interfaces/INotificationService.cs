using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using System;
using System.Threading.Tasks;

namespace Clinicia.Services.Interfaces
{
    public interface INotificationService
    {
        Task<PagedResult<Notification>> GetNotificationsAsync(int page, int pageSize, Guid userId);

        Task DeleteAsync(Guid id);

        void MarkAsRead(Guid id);

        int GetUnreadNotificationCount(Guid userId);

        void ResetUnreadNotificationCount(Guid userId);
    }
}
