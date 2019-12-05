using Clinicia.Services.Interfaces;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.MySql.Core;
using Hangfire.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Clinicia.WebApi.Schedulings
{
    public static class HangfireConfig
    {
        private const string ServerName = "clinicia";

        public static IServiceCollection AddHangfire([NotNull] this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseStorage(new MySqlStorage(connectionString, new MySqlStorageOptions
                {
                    TransactionTimeout = TimeSpan.FromMinutes(5),
                })));

            return services;
        }

        public static IApplicationBuilder UseHangfire([NotNull] this IApplicationBuilder app)
        {
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                ServerName = ServerName,
            });
            app.UseHangfireDashboard();

            InitRecurringJob();

            return app;
        }

        private static void InitRecurringJob()
        {
            var monitoringApi = JobStorage.Current.GetMonitoringApi();

            if (monitoringApi.Servers().Count > 0)
            {
                var serverToRemove = monitoringApi.Servers().First(x => x.Name.Contains(ServerName));
                if (serverToRemove != null)
                {
                    using (var connection = JobStorage.Current.GetConnection())
                    {
                        foreach (var recurringJob in connection.GetRecurringJobs())
                        {
                            RecurringJob.RemoveIfExists(recurringJob.Id);
                        }
                    }

                    JobStorage.Current.GetConnection().RemoveServer(serverToRemove.Name);
                }
            }

           RecurringJob.AddOrUpdate<ISchedulingService>(x => x.NotifyUpcomingAppointment(), Cron.Minutely);
        }
    }
}
