using Medical.Application.Services;
using Medical.CrossCutting.Common.Configs;
using Medical.CrossCutting.Common.Log;
using Medical.CrossCutting.Common.Services;
using Medical.Domain.Services;
using Medical.Infra.Data;
using Medical.Infra.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Medical.CrossCutting.IoC
{

    /// <summary>
    /// Classe de injeção de dependência
    /// </summary>
    public static class DependencyInjectionBootStrapper
    {

        /// <summary>
        /// Registrador de serviço de injeção de dependência
        /// </summary>
        /// <param name="services">Coleção de serviços para injeção</param>
        /// <param name="options">Configuration options</param>
        public static IServiceCollection AddMedicalAppService(this IServiceCollection services, IConfiguration configuration)
        {

            #region Configs

            services.Configure<AppConfig>(configuration.Bind);
            services.Configure<ConnectionStringsConfig>(configuration.GetSection("ConnectionStrings").Bind);
            services.Configure<OpeningHoursConfig>(configuration.GetSection("OpeningHours").Bind);

            #endregion

            #region Logs

            services.AddScoped<ILoggerFactory, AppLoggerFactory>();
            services.AddScoped<ILogger>(x => x.GetService<ILoggerFactory>().Create());

            #endregion

            #region DbContext

            services.AddDbContext<MedicalDbContext>(opt =>
            {
                opt
                    .UseSqlServer(configuration.GetConnectionString("DbServer"))
                    .UseLazyLoadingProxies()
                ;
            }, ServiceLifetime.Scoped);

            #endregion

            #region Infra.Data

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();


            #endregion

            #region Domain

            services.AddScoped<IAppointmentDomainService, AppointmentDomainService>();
            services.AddScoped<IDoctorDomainService, DoctorDomainService>();
            services.AddScoped<IPatientDomainService, PatientDomainService>();

            #endregion

            #region Application

            services.AddScoped<IAppointmentAppService, AppointmentAppService>();

            #endregion

            return services;

        }

        /// <summary>
        /// Execute migration in database
        /// </summary>
        /// <param name="app">Application builder object</param>
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using var context = serviceScope.ServiceProvider.GetService<MedicalDbContext>();
                context.Database.Migrate();
            }

            return app;
        }

    }

}
