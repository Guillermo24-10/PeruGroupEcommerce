namespace PeruGroup.Ecommerce.Services.WebApi.Extensiones.GlobalException
{
    public static class GlobalExceptionExtensions
    {
        public static IServiceCollection AddGlobalException(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandler>();

            return services;
        }
    }
}
