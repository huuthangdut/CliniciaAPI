using AutoMapper;
using Clinicia.Infrastructure.ContainerConfigs;
using Clinicia.Services.Helpers;
using Clinicia.WebApi.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinicia.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            CoreServicesInstaller.ConfigureCoreServices(services, Configuration);
            AuthServicesInstaller.ConfigureServicesAuth(services, Configuration);
            ApplicationServicesInstaller.ConfigureApplicationServices(services, Configuration);

            //Auto Mapper Configurations
            services.AddSingleton(provider => new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DtoToSchemaMappingProfile());
                mc.AddProfile(new SchemaToDtoMappingProfile());
                mc.AddProfile(new DtoToResultMappingProfile());
                mc.AddProfile(new ModelToDtoMappingProfile());
            }).CreateMapper());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
