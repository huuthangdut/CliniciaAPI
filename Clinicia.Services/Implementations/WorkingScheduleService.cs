using Clinicia.Dtos.Output;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;
using System;

namespace Clinicia.Services.Implementations
{
    public class WorkingScheduleService : IWorkingScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkingScheduleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }       

        public WorkingSchedule[] GetWorkingSchedules(Guid doctorId)
        {
            return _unitOfWork.WorkingScheduleRepository.GetWorkingSchedules(doctorId);
        }

        public void UpdateWorkingHour(Guid id, string hours)
        {
            var workingSchedule = _unitOfWork.WorkingScheduleRepository.Get(id);
            if(workingSchedule != null)
            {
                workingSchedule.Hours = hours;
                _unitOfWork.Complete();
            }
        }
    }
}
