using Microsoft.Extensions.DependencyInjection;
using Regularizacion.Application.Repository;
using Regularizacion.Infrastructure.Repository;


namespace Regularizacion.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRegularizacionServices(this IServiceCollection services)
        {
            services.AddScoped<IRegularizacionRepository, RegularizacionRepository>();
            return services;
        }
    }
}
