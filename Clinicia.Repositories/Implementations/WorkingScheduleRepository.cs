using AutoMapper;
using Clinicia.Common.Extensions;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Interfaces;
using Clinicia.Repositories.Schemas;
using System;
using System.Linq;

namespace Clinicia.Repositories.Implementations
{
    public class WorkingScheduleRepository : GenericRepository<DbWorkingSchedule>, IWorkingScheduleRepository
    {
        private readonly IMapper _mapper;

        public WorkingScheduleRepository(CliniciaDbContext context, IMapper mapper)
             : base(context)
        {
            _mapper = mapper;
        }

        public WorkingSchedule[] GetWorkingSchedules(Guid doctorId)
        {
            return Context.WorkingSchedules
                .Where(x => x.DoctorId == doctorId)
                .Select(x => new WorkingSchedule
                {
                    Id = x.Id.ToString(),
                    FromDate = x.FromDate,
                    IsActive = x.IsActive,
                    Hours = x.Hours.ToWeekWorkingTimes()
                }).ToArray();
        }
    }
}
