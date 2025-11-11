using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Application.Interface.Persistence;
using PeruGroup.Ecommerce.Persistence.Contexts;
using PeruGroup.Ecommerce.Persistence.Interceptors;
using PeruGroup.Ecommerce.Persistence.Repositories;

namespace PeruGroup.Ecommerce.Infrastructure.Repository.Extensiones
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("NorthwindConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<ICustomersRepository, CustomersRepository>();            
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
