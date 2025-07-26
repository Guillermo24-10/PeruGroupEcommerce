using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PeruGroup.Ecommerce.Services.WebApi.Extensiones.HealthCheck
{
    public class HealthCheckCustom : IHealthCheck
    {
        private readonly Random _random = new();
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var responseTime = _random.Next(1, 300);
            if (responseTime < 100)
            {
                return Task.FromResult(HealthCheckResult.Healthy("The service is healthy."));
            }
            else if (responseTime < 200)
            {
                return Task.FromResult(HealthCheckResult.Degraded("The service is degraded."));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("The service is unhealthy."));
            }
        }
    }
}
