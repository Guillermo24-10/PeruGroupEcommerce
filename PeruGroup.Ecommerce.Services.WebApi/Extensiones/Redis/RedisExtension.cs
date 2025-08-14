namespace PeruGroup.Ecommerce.Services.WebApi.Extensiones.Redis
{
    public static class RedisExtension
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetConnectionString("RedisConnection");
            if (string.IsNullOrEmpty(redisConnectionString))
            {
                throw new ArgumentException("Verificar la conexion a Redis");
            }
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });
            return services;
        }
    }
}
