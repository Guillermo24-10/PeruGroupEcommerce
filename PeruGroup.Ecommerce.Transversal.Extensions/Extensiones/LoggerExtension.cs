using Microsoft.Extensions.DependencyInjection;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Transversal.Logging.Extensiones
{
    public static class LoggerExtension
    {
        public static IServiceCollection AddLogger(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
