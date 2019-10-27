using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;
using System;
using System.Threading.Tasks;

namespace Clinicia.Repositories.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<DbAppointment>
    {
        Task<PagedResult<Appointment>> GetAppointmentsAsync(Guid userId, int page, int pageSize);

        Task<Appointment> GetAppointmentAsync(Guid id);
    }
}