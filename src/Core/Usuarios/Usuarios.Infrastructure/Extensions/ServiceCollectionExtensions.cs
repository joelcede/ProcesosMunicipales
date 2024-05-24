using Microsoft.Extensions.DependencyInjection;
using Usuarios.Application.Repository;
using Usuarios.Infrastructure.Repository;

namespace Usuarios.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUsuarioServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            return services;
        }
        public static IServiceCollection AddCuentaMunicipalServices(this IServiceCollection services)
        {
            services.AddScoped<ICuentaMunicipalRepository, CuentaMunicipalRepository>();
            return services;
        }
    }
}
