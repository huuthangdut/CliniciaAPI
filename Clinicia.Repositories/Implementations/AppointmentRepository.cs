using AutoMapper;
using Clinicia.Common.Exceptions;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Helpers.Linq;
using Clinicia.Repositories.Interfaces;
using Clinicia.Repositories.Schemas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Clinicia.Common.Enums;
using Clinicia.Common.Extensions;

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
                .ThenInclude(d => d.Location)
                .Where(x => x.PatientId == userId && x.IsActive)
                .WhereIf(status.Length > 0, x => status.Select(s => (int)s).Contains(x.Status))
                .OrderByElseDescending(status.Length > 0 && (status.Contains(AppointmentStatus.Confirming) || status.Contains(AppointmentStatus.Confirming)), x => x.AppointmentDate)
                .GetPagedResultAsync(
                    page,
                    pageSize,
                    x => _mapper.Map<Appointment>(x)
                );
        }

        public async Task<Appointment> GetAppointmentAsync(Guid id)
        {
            var appointment = await Context.Appointments
                .Include(x => x.Doctor)
                .ThenInclude(d => d.Location)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            if (appointment == null)
            {
                throw new EntityNotFoundException(typeof(Appointment), id);
            }

            return _mapper.Map<Appointment>(appointment);
        }
    }
}
