using Microsoft.Extensions.DependencyInjection;

namespace PeruGroup.Ecommerce.Transversal.Mapper.Extensiones
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            // Automapper o mapeos personalizados
            services.AddAutoMapper(typeof(MappingsProfile).Assembly);

            return services;
        }
    }
}
