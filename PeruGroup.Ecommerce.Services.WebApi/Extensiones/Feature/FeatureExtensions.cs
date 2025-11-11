using System.Text.Json.Serialization;

namespace PeruGroup.Ecommerce.Services.WebApi.Extensiones.Feature
{
    public static class FeatureExtensions
    {
        public static IServiceCollection AddFeature(this IServiceCollection services, IConfiguration configuration)
        {
            var policy = "policyApiEcommerce";
            services.AddCors(options =>
            {
                options.AddPolicy(policy,
                    builder => builder.WithOrigins(configuration["Config:OriginCors"]!)
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            services.AddMvc();
            services.AddControllers().AddJsonOptions(opts =>
            {
                var enumConverter = new JsonStringEnumConverter();
                opts.JsonSerializerOptions.Converters.Add(enumConverter);
            });

            return services;
        }
    }
}
