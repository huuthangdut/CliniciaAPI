using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;
using System;

namespace Clinicia.Repositories.Interfaces
{
    public interface IWorkingScheduleRepository : IGenericRepository<DbWorkingSchedule>
    {
        WorkingSchedule[] GetWorkingSchedules(Guid doctorId);
    }
}
