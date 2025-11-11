using MassTransit;
using PeruGroup.Ecommerce.Application.Interface.Infrastructure;

namespace PeruGroup.Ecommerce.Infrastructure.EventBus
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint; // para publicar eventos en el broker de mensajes

        public EventBusRabbitMQ(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async void Publish<T>(T @event)
        {
            await _publishEndpoint.Publish(@event!);
        }
    }
}
