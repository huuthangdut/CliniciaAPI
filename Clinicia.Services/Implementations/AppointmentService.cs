using AutoMapper;
using Clinicia.Common.Enums;
using Clinicia.Common.Exceptions;
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
        private readonly IPushNotificationService _pushNotificationService;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper, IPushNotificationService pushNotificationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _pushNotificationService = pushNotificationService;
        }

        public async Task<PagedResult<Appointment>> GetAppointmentsAsync(Guid userId, int page, int pageSize, AppointmentStatus[] status)
        {
            return await _unitOfWork.AppointmentRepository.GetAppointmentsAsync(userId, page, pageSize, status);
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

        public async Task UpdateStatus(Guid id, AppointmentStatus status)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetFirstOrDefaultAsync(x => x.Id == id, x => x.Doctor) ?? throw new EntityNotFoundException(typeof(DbAppointment), id);
            appointment.Status = (int)status;

            string title = "";
            string message = "";

            switch(status)
            {
                case AppointmentStatus.Cancelled:
                    title = "Huỷ lịch hẹn";
                    message = $"Bác sĩ {appointment.Doctor.FirstName} {appointment.Doctor.LastName} đã huỷ lịch hẹn ngày {appointment.AppointmentDate.ToString("yyyy/MM/dd HH:mm")}. Chúng tôi rất tiếc vì sự cố này.";
                    break;
                case AppointmentStatus.Completed:
                    title = "Hoàn thành lịch hẹn";
                    message = $"Bạn đã hoàn thành lịch hẹn ngày {appointment.AppointmentDate.ToString("yyyy/MM/dd HH:mm")} với bác sĩ {appointment.Doctor.FirstName} {appointment.Doctor.LastName}. Vui lòng đánh giá bác sĩ tại đây.";
                    break;
                case AppointmentStatus.Confirmed:
                    title = "Xác nhận lịch hẹn";
                    message = $"Bác sĩ {appointment.Doctor.FirstName} {appointment.Doctor.LastName} đã xác nhận lịch hẹn ngày {appointment.AppointmentDate.ToString("yyyy/MM/dd HH:mm")} của bạn.";
                    break;
                default:
                    await _unitOfWork.CompleteAsync();
                    return;
            }

            var notification = new DbNotification
            {
                UserId = appointment.PatientId,
                Title = title,
                Content = message,
                NotificationDate = DateTime.Now,
                HasRead = false,
                Image = appointment.Doctor.ImageProfile,
                AppointmentId = appointment.Id
            };
            await _unitOfWork.NotificationRepository.AddAsync(notification).ContinueWith(x => _unitOfWork.Complete());
            await _pushNotificationService.SendToAllDeviceUsers(appointment.PatientId, new FcmPayloadNotification
            {
                Notification = new FcmNotification
                {
                    Title = notification.Title,
                    Body = notification.Content
                },
                Data = new FcmDataNotification
                {
                    Id = notification.Id.ToString(),
                    Image = notification.Image,
                    NotificationDate = notification.NotificationDate.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    AppointmentId = appointment.Id.ToString()
                }
            });

            await _unitOfWork.CompleteAsync();
        }

        public async Task<Appointment> GetUpcomingAppointment(Guid userId)
        {
            return await _unitOfWork.AppointmentRepository.GetUpcomingAppointment(userId);
        }
    }
}
