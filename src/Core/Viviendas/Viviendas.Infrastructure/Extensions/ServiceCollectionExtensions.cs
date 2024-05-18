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
        public static IServiceCollection AddViviendaUsuarioServices(this IServiceCollection services)
        {
            services.AddScoped<IViviendaUsuarioRepository, ViviendaUsuarioRepository>();
            return services;
        }
        public static IServiceCollection AddViviendaImagenServices(this IServiceCollection services)
        {
            services.AddScoped<IViviendaImagenRepository, ViviendaImagenRepository>();
            return services;
        }
    }
}
