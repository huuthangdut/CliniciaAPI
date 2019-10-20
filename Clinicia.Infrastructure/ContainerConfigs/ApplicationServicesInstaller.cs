using Clinicia.Common.Runtime.Claims;
using Clinicia.Repositories.Audits;
using Clinicia.Repositories.UnitOfWork;
using Clinicia.Services.Implementations;
using Clinicia.Services.Interfaces;
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
            services.AddHttpContextAccessor();

            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<ITokenService, JwtTokenService>();
            services.AddTransient<IRegisterService, RegisterService>();
            services.AddTransient<ISpecialtyService, SpecialtyService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IDoctorService, DoctorService>();
        }
    }
}