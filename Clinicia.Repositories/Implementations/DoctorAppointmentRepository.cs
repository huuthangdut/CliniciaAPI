using AutoMapper;
using Clinicia.Common.Enums;
using Clinicia.Common.Exceptions;
using Clinicia.Common.Extensions;
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
    public class DoctorAppointmentRepository : GenericRepository<DbAppointment>, IDoctorAppointmentRepository
    {
        private readonly IMapper _mapper;

        public DoctorAppointmentRepository(CliniciaDbContext context, IMapper mapper) 
            : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedResult<DoctorAppointment>> GetDoctorAppointmentsAsync(Guid doctorId, int page, int pageSize, AppointmentStatus[] status)
        {
            return await Context.Appointments
                .Include(x => x.CheckingService)
                .Include(x => x.Patient)
                .ThenInclude(d => d.Location)
                .Where(x => x.DoctorId == doctorId && x.IsActive)
                .WhereIf(status.Length > 0, x => status.Select(s => (int)s).Contains(x.Status))
                .OrderByDescending(x => x.AppointmentDate)
                .GetPagedResultAsync(
                    page,
                    pageSize,
                    x => _mapper.Map<DoctorAppointment>(x)
                );
        }

        public async Task<DoctorAppointment> GetDoctorAppointmentAsync(Guid id)
        {
            var appointment = await Context.Appointments
                .Include(x => x.CheckingService)
                .Include(x => x.Patient)
                .ThenInclude(d => d.Location)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            if (appointment == null)
            {
                throw new EntityNotFoundException(typeof(DoctorAppointment), id);
            }

            return _mapper.Map<DoctorAppointment>(appointment);
        }
    }
}
