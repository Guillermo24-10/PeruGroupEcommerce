using Microsoft.Extensions.DependencyInjection;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Application.Validator;

namespace PeruGroup.Ecommerce.Application.Main.Extensiones
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<ICategoriesApplication, CategoriesApplication>();
            services.AddTransient<UsersDtoValidator>();

            return services;
        }
    }
}
