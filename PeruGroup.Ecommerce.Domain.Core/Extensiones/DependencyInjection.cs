using Microsoft.Extensions.DependencyInjection;
using PeruGroup.Ecommerce.Domain.Interface;

namespace PeruGroup.Ecommerce.Domain.Core.Extensiones
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<ICustomersDomain, CustomersDomain>();
            services.AddScoped<IUsersDomain, UsersDomain>();

            return services;
        }
    }
}
