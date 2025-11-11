using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PeruGroup.Ecommerce.Application.Interface.Infrastructure;
using PeruGroup.Ecommerce.Infrastructure.EventBus;
using PeruGroup.Ecommerce.Infrastructure.EventBus.Options;
using PeruGroup.Ecommerce.Infrastructure.Notification;
using PeruGroup.Ecommerce.Infrastructure.Notification.Options;
using SendGrid.Extensions.DependencyInjection;

namespace PeruGroup.Ecommerce.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.ConfigureOptions<RabbitMqOptionsSetup>();
            services.AddScoped<IEventBus, EventBusRabbitMQ>();
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    RabbitMqOptions? opt = services.BuildServiceProvider().GetRequiredService<IOptions<RabbitMqOptions>>().Value;

                    cfg.Host(opt.HostName, opt.VirtualHost, h =>
                    {
                        h.Username(opt.UserName!);
                        h.Password(opt.Password!);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
            
            /*SERVICIO DE SENDGRID*/
            services.AddScoped<INotification,NotificationSendGrid>();
            services.ConfigureOptions<SendgridOptionsSetup>();
            SendgridOptions? sendgridOptions = services.BuildServiceProvider()
                .GetRequiredService<IOptions<SendgridOptions>>().Value;

            services.AddSendGrid(options =>
            {
                options.ApiKey = sendgridOptions.ApiKey;
            });

            return services;
        }
    }
}
