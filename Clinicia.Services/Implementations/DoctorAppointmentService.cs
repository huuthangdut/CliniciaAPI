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
    public class DoctorAppointmentService : IDoctorAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorAppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<DoctorAppointment>> GetDoctorAppointmentsAsync(Guid doctorId, int page, int pageSize, AppointmentStatus[] status)
        {
            return await _unitOfWork.DoctorAppointmentRepository.GetDoctorAppointmentsAsync(doctorId, page, pageSize, status);
        }

        public async Task<DoctorAppointment> GetDoctorAppointmentAsync(Guid id)
        {
            return await _unitOfWork.DoctorAppointmentRepository.GetDoctorAppointmentAsync(id);
        }

        public async Task UpdateStatus(Guid id, AppointmentStatus status)
        {
            var appointment = _unitOfWork.AppointmentRepository.Get(id);
            appointment.Status = (int)status;

            await _unitOfWork.CompleteAsync();
            
        }
    }
}
