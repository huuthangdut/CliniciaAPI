using AutoMapper;
using Clinicia.Common.Enums;
using Clinicia.Dtos.Common;
using Clinicia.Dtos.Input;
using Clinicia.Dtos.Output;
using Clinicia.Repositories.Schemas;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Clinicia.Services.Implementations
{
    public class CheckingService : ICheckingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CheckingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<Appointment>> GetAppointmentsAsync(Guid userId, int page, int pageSize, AppointmentStatus[] status)
        {
            return await _unitOfWork.AppointmentRepository.GetAppointmentsAsync(userId, page, pageSize, status);
        }

        public DoctorCheckingService[] GetCheckingServices(Guid doctorId)
        {
            return _unitOfWork.CheckingServiceRepository.GetCheckingServices(doctorId);
        }

        public async Task AddCheckingServiceAsync(CreatedCheckingService checkingService)
        {
            var model = _mapper.Map<DbCheckingService>(checkingService);
            await _unitOfWork.CheckingServiceRepository.AddAsync(model);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateCheckingServiceAsync(UpdatedCheckingService checkingService)
        {
            var model = _unitOfWork.CheckingServiceRepository.Get(checkingService.Id);
            model.Name = checkingService.Name;
            model.Price = checkingService.Price;
            model.DurationInMinutes = checkingService.DurationInMinutes;
            model.Description = checkingService.Description;

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCheckingServiceAsync(Guid id)
        {
            await _unitOfWork.CheckingServiceRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
