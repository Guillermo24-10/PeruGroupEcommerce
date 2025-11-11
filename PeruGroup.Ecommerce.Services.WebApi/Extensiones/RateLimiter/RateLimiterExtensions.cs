using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace PeruGroup.Ecommerce.Services.WebApi.Extensiones.RateLimiter
{
    public static class RateLimiterExtensions
    {
        public static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            var fixedWindowPolicy = "FixedWindow";

            services.AddRateLimiter(configureOptions =>
            {
                configureOptions.AddFixedWindowLimiter(policyName: fixedWindowPolicy, fixedWindow =>
                {
                    fixedWindow.PermitLimit = int.Parse(configuration["RateLimiting:PermitLimit"]!); // Número máximo de solicitudes permitidas en la ventana de tiempo 
                    fixedWindow.Window = TimeSpan.FromSeconds(int.Parse(configuration["RateLimiting:Window"]!)); // Ventana de tiempo de 1 minuto
                    fixedWindow.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; // Procesar las solicitudes en cola en orden de llegada
                    fixedWindow.QueueLimit = int.Parse(configuration["RateLimiting:QueueLimit"]!); // indica la cantidad de solicitudes que se pueden poner en cola cuando se alcanza el límite de permisos. En este caso, está configurado a 0, lo que significa que no se permiten solicitudes en cola.  
                });
                configureOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests; // Código de estado HTTP para respuestas rechazadas.
            });            
            return services;
        }

    }
}
