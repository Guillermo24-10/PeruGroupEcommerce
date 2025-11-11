using PeruGroup.Ecommerce.Services.WebApi.Extensiones.GlobalException;

namespace PeruGroup.Ecommerce.Services.WebApi.Extensiones.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder AddMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}
