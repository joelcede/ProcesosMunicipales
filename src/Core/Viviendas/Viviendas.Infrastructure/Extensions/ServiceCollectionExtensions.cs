using Microsoft.Extensions.DependencyInjection;
using Viviendas.Application.Repository;
using Viviendas.Infrastructure.Repository;


namespace Usuarios.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddViviendaServices(this IServiceCollection services)
        {
            services.AddScoped<IViviendaRepository, ViviendaRepository>();
            return services;
        }
        public static IServiceCollection AddViviendaITServices(this IServiceCollection services)
        {
            services.AddScoped<IViviendaTIRepository, ViviendaITRespository>();
            return services;
        }
    }
}
