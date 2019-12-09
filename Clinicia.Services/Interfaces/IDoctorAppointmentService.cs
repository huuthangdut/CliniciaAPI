using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using System;
using System.Threading.Tasks;
using Clinicia.Common.Enums;

namespace Clinicia.Services.Interfaces
{
    public interface IDoctorAppointmentService
    {
        Task<PagedResult<DoctorAppointment>> GetDoctorAppointmentsAsync(Guid doctorId, int page, int pageSize, AppointmentStatus[] status);

        Task<DoctorAppointment> GetDoctorAppointmentAsync(Guid id);

        Task UpdateStatus(Guid id, AppointmentStatus status);
    }
}