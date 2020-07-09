using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Medical.Web.Api
{

    /// <summary>
    /// Web Application service extension
    /// </summary>
    public static class ServiceExtension
    {

        /// <summary>
        /// Add swager application config
        /// </summary>
        /// <param name="services">Service collection</param>
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Medical Services Api",
                        Version = "v1",
                        Description = "Medical Services (Doctor / Patient)",
                        Contact = new OpenApiContact
                        {
                            Name = "Rodrigo Rodrigues",
                            Url = new Uri("https://github.com/rodriguesrm")
                        }
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            return services;

        }

    }
}
