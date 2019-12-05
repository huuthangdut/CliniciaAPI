using System;
using System.Threading.Tasks;
using Clinicia.Common.Extensions;
using Clinicia.Dtos.Common;
using Clinicia.Repositories.Projections;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Interfaces;

namespace Clinicia.Services.Implementations
{
    public class SchedulingService : ISchedulingService
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IUnitOfWork _unitOfWork;

        public SchedulingService(IPushNotificationService pushNotificationService, IUnitOfWork unitOfWork)
        {
            _pushNotificationService = pushNotificationService;
            _unitOfWork = unitOfWork;
        }

        public async Task NotifyUpcomingAppointment()
        {
            try
            {
                var reminderAppointments = await _unitOfWork.AppointmentRepository.GetReminderAppointments();
                if (reminderAppointments.Length > 0)
                {
                    var notifications = reminderAppointments.ConvertArray(x => new DbNotificationProjection
                    {
                        UserId = x.UserId,
                        Title = "Nhắc nhở lịch hẹn",
                        Content = $"Bạn có lịch hẹn với bác sĩ {x.DoctorName} vào lúc {x.AppointmentDate.ToString("HH:mm dd/MM/yyyy")}.",
                        Image = x.DoctorImage,
                        NotificationDate = DateTime.Now,
                        HasRead = false,
                        Devices = x.Devices
                    });

                    await _unitOfWork.NotificationRepository
                        .AddRangeAsync(notifications)
                        .ContinueWith(x => _unitOfWork.Complete());

                    foreach (var notification in notifications)
                    {
                        foreach (var device in notification.Devices)
                        {
                            await _pushNotificationService.SendAsync(device.DeviceToken, new FcmPayloadNotification
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
                                    NotificationDate = notification.NotificationDate.ToString("yyyy-MM-ddTHH:mm:ssZ")
                                }
                            });
                        }
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
