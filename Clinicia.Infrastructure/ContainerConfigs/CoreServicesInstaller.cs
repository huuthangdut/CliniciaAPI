using System.Collections.Generic;
using System.Linq;
using Clinicia.Common.Enums;
using Clinicia.Common.Extensions;
using Clinicia.Infrastructure.ApiResults;
using Clinicia.Infrastructure.Filters;
using Clinicia.Repositories;
using Clinicia.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Clinicia.Infrastructure.ContainerConfigs
{
    public static class CoreServicesInstaller
    {
        public static void ConfigureCoreServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMvc(
                options =>
                    options.Filters.Add(typeof(HttpGlobalExceptionFilter))
                )
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2); ;

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.Configure<ApiBehaviorOptions>(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var error = actionContext.ModelState
                            .FirstOrDefault(x => x.Value.Errors.Any())
                            .Value
                            .Errors.FirstOrDefault();

                        return new BadRequestObjectResult(
                            new ApiErrorResult
                            {
                                Success = false,
                                ErrorCode = ErrorCodes.InvalidParameters.ToString(),
                                ErrorMessage = error?.ErrorMessage != null && error.ErrorMessage.IsNullOrEmpty() ? error.Exception.Message : error?.ErrorMessage,
                            });
                    };
             });

            services.AddDbContext<CliniciaDbContext>(options => options.UseMySql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ModelValidationFilterAttribute>();

            services.AddMemoryCache();

            services.AddCors();
        }
    }
}