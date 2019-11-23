using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Helpers.Linq;
using Clinicia.Repositories.Interfaces;
using Clinicia.Repositories.Schemas;

namespace Clinicia.Repositories.Implementations
{
    public class NotificationRepository : GenericRepository<DbNotification>, INotificationRepository
    {
        private readonly IMapper _mapper;

        public NotificationRepository(CliniciaDbContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public Task<PagedResult<Notification>> GetNotificationsAsync(int page, int pageSize, Guid userId)
        {
            return Context.Notifications
                .Where(x => x.IsActive && (x.UserId == userId || x.UserId == null))
                .OrderByDescending(x => x.NotificationDate)
                .GetPagedResultAsync(page, pageSize, x => _mapper.Map<Notification>(x));
        }
    }
}
