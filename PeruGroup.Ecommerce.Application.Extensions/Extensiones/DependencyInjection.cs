using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PeruGroup.Ecommerce.Application.Interface.UseCases;
using PeruGroup.Ecommerce.Application.UseCases.Categories;
using PeruGroup.Ecommerce.Application.UseCases.Common.Behavirours;
using PeruGroup.Ecommerce.Application.UseCases.Common.Mappings;
using PeruGroup.Ecommerce.Application.UseCases.Customers;
using PeruGroup.Ecommerce.Application.UseCases.Discount;
using PeruGroup.Ecommerce.Application.UseCases.Users;
using PeruGroup.Ecommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand;

namespace PeruGroup.Ecommerce.Application.Main.Extensiones
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(CreateUserTokenCommandHandler).Assembly;

            services.AddValidatorsFromAssembly(assembly);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.RegisterServicesFromAssembly(typeof(CreateUserTokenCommandHandler).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            });
            services.AddAutoMapper(typeof(MappingsProfile));
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<ICategoriesApplication, CategoriesApplication>();
            services.AddScoped<IDiscountApplication, DiscountApplication>();

            return services;
        }
    }
}
