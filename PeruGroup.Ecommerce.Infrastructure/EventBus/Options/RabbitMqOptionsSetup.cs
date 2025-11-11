using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace PeruGroup.Ecommerce.Infrastructure.EventBus.Options
{
    public class RabbitMqOptionsSetup : IConfigureOptions<RabbitMqOptions>
    {
        public const string ConfigurationSectionName = "RabbitMqOptions";
        private readonly IConfiguration _configuration;

        public RabbitMqOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(RabbitMqOptions options)
        {
            _configuration.GetSection(ConfigurationSectionName).Bind(options); // vincular la sección de configuración a la instancia de opciones
        }
    }
}
