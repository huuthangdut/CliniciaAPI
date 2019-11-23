using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;
using System;
using System.Threading.Tasks;

namespace Clinicia.Repositories.Interfaces
{
    public interface INotificationRepository : IGenericRepository<DbNotification>
    {
        Task<PagedResult<Notification>> GetNotificationsAsync(int page, int pageSize, Guid userId);
    }
}
