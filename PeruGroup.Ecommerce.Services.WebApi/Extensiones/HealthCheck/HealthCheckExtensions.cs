namespace PeruGroup.Ecommerce.Services.WebApi.Extensiones.HealthCheck
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("NorthwindConnection")!, tags: new[] { "database" })
                .AddRedis(configuration.GetConnectionString("RedisConnection")!, tags: new[] { "cache" })
                .AddCheck<HealthCheckCustom>("HealthCheckCustom", tags: new[] { "custom" });
            services.AddHealthChecksUI().AddSqlServerStorage(configuration.GetConnectionString("NorthwindConnection")!);

            return services;
        }
    }
}
