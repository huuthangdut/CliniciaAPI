using Clinicia.Dtos.Output;
using System;

namespace Clinicia.Services.Interfaces
{
    public interface IWorkingScheduleService
    {
        WorkingSchedule[] GetWorkingSchedules(Guid doctorId);

        void UpdateWorkingHour(Guid id, string hours);
    }
}
