using Clinicia.Common.Runtime.Claims;
using Clinicia.Repositories.Audits;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Implementations;
using Clinicia.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinicia.Infrastructure.ContainerConfigs
{
    public static class ApplicationServicesInstaller
    {
        public static void ConfigureApplicationServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuditHelper, AuditHelper>();
            services.AddScoped<IClaimsIdentity, ClaimsIdentity>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<ITwoFactorAuthenticationService, TwoFactorAuthenticationService>();
            services.AddTransient<ITokenService, JwtTokenService>();
            services.AddTransient<IRegisterService, RegisterService>();
            services.AddTransient<ISpecialtyService, SpecialtyService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IFavoriteService, FavoriteService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IPushNotificationService, FcmService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<ISmsService, TwilioService>();
            services.AddTransient<ISchedulingService, SchedulingService>();
            services.AddTransient<IDoctorAppointmentService, DoctorAppointmentService>();
            services.AddTransient<ICheckingService, CheckingService>();
            services.AddTransient<IWorkingScheduleService, WorkingScheduleService>();
        }
    }
}