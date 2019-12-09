using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;
using System;
using System.Threading.Tasks;
using Clinicia.Common.Enums;

namespace Clinicia.Repositories.Interfaces
{
    public interface IDoctorAppointmentRepository : IGenericRepository<DbAppointment>
    {
        Task<PagedResult<DoctorAppointment>> GetDoctorAppointmentsAsync(Guid userId, int page, int pageSize, AppointmentStatus[] status);

        Task<DoctorAppointment> GetDoctorAppointmentAsync(Guid id);
    }
}