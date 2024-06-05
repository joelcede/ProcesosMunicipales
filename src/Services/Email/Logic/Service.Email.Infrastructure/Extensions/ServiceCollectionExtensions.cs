using Microsoft.Extensions.DependencyInjection;
using Service.Email.Application.Repository;
using Service.Email.Infrastructure.Repository;


namespace Service.Email.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCambioEstadoRegularizacion(this IServiceCollection services)
        {
            services.AddScoped<IPendientesRepository, PendientesRepository>();
            return services;
        }
    }
}
