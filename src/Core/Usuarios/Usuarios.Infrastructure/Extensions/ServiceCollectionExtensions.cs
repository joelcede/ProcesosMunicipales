using Microsoft.Extensions.DependencyInjection;
using Usuarios.Application.Repository;
using Usuarios.Infrastructure.Repository;

namespace Usuarios.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClienteServices(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            return services;
        }
        public static IServiceCollection AddFamiliarServices(this IServiceCollection services)
        {
            services.AddScoped<IFamiliarRepository, FamiliarRepository>();
            return services;
        }
        public static IServiceCollection AddPropietarioSerices(this IServiceCollection services)
        {
            services.AddScoped<IPropietarioRepository, PropietarioRepository>();
            return services;
        }
    }
}
