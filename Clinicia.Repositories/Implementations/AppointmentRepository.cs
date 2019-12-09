using AutoMapper;
using Clinicia.Common.Enums;
using Clinicia.Common.Exceptions;
using Clinicia.Common.Extensions;
using Clinicia.Common.Helpers;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Helpers.Linq;
using Clinicia.Repositories.Interfaces;
using Clinicia.Repositories.Schemas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Clinicia.Repositories.Implementations
{
    public class AppointmentRepository : GenericRepository<DbAppointment>, IAppointmentRepository
    {
        private readonly IMapper _mapper;

        public AppointmentRepository(CliniciaDbContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedResult<Appointment>> GetAppointmentsAsync(Guid userId, int page, int pageSize, AppointmentStatus[] status)
        {
            return await Context.Appointments
                .Include(x => x.CheckingService)
                .Include(x => x.Doctor)
                    .ThenInclude(x => x.Specialty)
                .Include(x => x.Doctor)
                    .ThenInclude(d => d.Location)
                .Where(x => x.PatientId == userId && x.IsActive)
                .WhereIf(status.Length > 0, x => status.Select(s => (int)s).Contains(x.Status))
                .OrderByElseDescending(status.Length > 0 && (status.Contains(AppointmentStatus.Confirming) || status.Contains(AppointmentStatus.Confirmed)), x => x.AppointmentDate)
                .GetPagedResultAsync(
                    page,
                    pageSize,
                    x => _mapper.Map<Appointment>(x)
                );
        }

        public async Task<Appointment> GetAppointmentAsync(Guid id)
        {
            var appointment = await Context.Appointments
                .Include(x => x.CheckingService)
               .Include(x => x.Doctor)
                    .ThenInclude(x => x.Specialty)
                .Include(x => x.Doctor)
                    .ThenInclude(d => d.Location)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            if (appointment == null)
            {
                throw new EntityNotFoundException(typeof(Appointment), id);
            }

            return _mapper.Map<Appointment>(appointment);
        }

        public async Task<ReminderAppointment[]> GetReminderAppointments()
        {
          return await Context.Appointments
                .Include(x => x.CheckingService)
                .Include(x => x.Doctor)
                .Include(x => x.Patient)
                    .ThenInclude(x => x.Devices)
                .Where(x => 
                    x.Patient.PushNotificationEnabled &&
                    x.Status == (int)AppointmentStatus.Confirmed && 
                    (x.AppointmentDate - DateTime.Now).TotalMinutes == x.SendNotificationBeforeMinutes &&
                    x.Patient.Devices.Any(device => device.IsActive && device.ExpiredAt > DateTime.UtcNow))
                .Select(x => new ReminderAppointment
                {
                    UserId = x.PatientId,
                    DoctorName = $"{x.Doctor.FirstName} {x.Doctor.LastName}",
                    DoctorImage = x.Doctor.ImageProfile,
                    AppointmentDate = x.AppointmentDate,
                    Devices = x.Patient.Devices
                        .Where(device => device.IsActive && device.ExpiredAt > DateTime.UtcNow)
                        .ConvertArray(device => new Device
                        {
                            Id = device.Id,
                            DeviceType = device.DeviceType,
                            DeviceToken = device.DeviceToken
                        })
                }).ToArrayAsync();
        }

        public async Task<Appointment> GetUpcomingAppointment(Guid userId)
        {
            var appointment = await Context.Appointments
                .Include(x => x.CheckingService)
               .Include(x => x.Doctor)
                    .ThenInclude(x => x.Specialty)
                .Include(x => x.Doctor)
                    .ThenInclude(d => d.Location)
                .Include(x => x.Patient)
                    .ThenInclude(x => x.Location)
               .Where(x => x.PatientId == userId && x.IsActive && x.Status == (int)AppointmentStatus.Confirmed)
               .OrderBy(x => x.AppointmentDate)
               .FirstOrDefaultAsync();

            var distanceFromPatient = LocationHelper.GetDistance(appointment.Patient.Location.Latitude, appointment.Patient.Location.Longitude, appointment.Doctor.Location.Latitude, appointment.Doctor.Location.Longitude) / 1000;

            if (appointment == null)
            {
                throw new EntityNotFoundException();
            }

            var result = _mapper.Map<Appointment>(appointment);
            result.Doctor.DistanceFromPatient = distanceFromPatient;

            return result;
        }
    }
}
