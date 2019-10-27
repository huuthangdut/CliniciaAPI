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
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<Appointment>> GetAppointmentsAsync(Guid userId, int page, int pageSize)
        {
            return await _unitOfWork.AppointmentRepository.GetAppointmentsAsync(userId, page, pageSize);
        }

        public async Task<Appointment> AddAppointmentAsync(Guid userId, CreatedAppointment model)
        {
            var appointment = _mapper.Map<DbAppointment>(model);
            appointment.Status = (int) AppointmentStatus.Confirming;
            appointment.PatientId = userId;

            var addedAppointment = await _unitOfWork.AppointmentRepository.AddAsync(appointment);

            await _unitOfWork.CompleteAsync();

            return await GetAppointmentAsync(addedAppointment.Id);
        }

        public async Task<Appointment> GetAppointmentAsync(Guid id)
        {
            return await _unitOfWork.AppointmentRepository.GetAppointmentAsync(id);
        }
    }
}
