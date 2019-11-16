using Clinicia.Dtos.Common;
using Clinicia.Dtos.Input;
using Clinicia.Dtos.Output;
using System;
using System.Threading.Tasks;
using Clinicia.Common.Enums;

namespace Clinicia.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<PagedResult<Appointment>> GetAppointmentsAsync(Guid userId, int page, int pageSize, AppointmentStatus[] status);

        Task<Appointment> AddAppointmentAsync(Guid userId, CreatedAppointment appointment);

        Task<Appointment> GetAppointmentAsync(Guid id);
    }
}