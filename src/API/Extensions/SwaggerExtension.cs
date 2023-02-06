using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace API.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var openApi = new OpenApiInfo
            {
                Title = "Api crud Experis",
                Version = "V1.0.0",
                Description = "Microservicios",
                TermsOfService = new Uri("https://opensource.org/licenses/MIT"),
                Contact = new OpenApiContact
                {
                    Name = "Team Developer",
                    Email = "edson.panduro@tecsup.edu.pe",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                },
                License = new OpenApiLicense
                {
                    Name = "Uso solo de prueba",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            };


            services.AddSwaggerGen(x =>
            {
                openApi.Version = "V1";
                x.SwaggerDoc("v1", openApi);
            });

            return services;
        }
    }
}
