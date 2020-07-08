using Medical.CrossCutting.Common.Services;
using Medical.Domain.Services;
using Medical.Infra.Data;
using Medical.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            #region DbContext

            services.AddDbContext<MedicalContext>(opt =>
            {
                opt
                    .UseSqlServer(configuration.GetConnectionString("DbServer"))
                    .UseLazyLoadingProxies()
                ;
            }, ServiceLifetime.Scoped);

            #endregion

            #region Infra.Data

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region Repository

            services.AddScoped<IAppClientRepository, AppClientRepository>();


            #endregion

            #region Domain

            services.AddScoped<IAppClientDomainService, AppClientDomainService>();

            #endregion

            #region Application

            //services.AddScoped<ITransactionApplicationService, TransactionApplicationService>();

            #endregion

            return services;

        }

    }

}
