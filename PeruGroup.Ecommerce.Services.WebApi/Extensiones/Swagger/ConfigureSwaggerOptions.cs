using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PeruGroup.Ecommerce.Services.WebApi.Extensiones.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        static OpenApiInfo CreateApiVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "PeruGroup Ecommerce API",
                Version = description.ApiVersion.ToString(),
                Description = "API for managing ecommerce operations.",
                Contact = new OpenApiContact
                {
                    Name = "PeruGroup Support",
                    Email = ""
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " Esta version de la API ha quedado obsoleta.";
            }

            return info;
        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateApiVersionInfo(description));
            }
        }
    }
}
