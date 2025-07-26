using Microsoft.Extensions.DependencyInjection;
using PeruGroup.Ecommerce.Infrastructure.Data;
using PeruGroup.Ecommerce.Infrastructure.Interface;

namespace PeruGroup.Ecommerce.Infrastructure.Repository.Extensiones
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
