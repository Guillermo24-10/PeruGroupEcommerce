using Asp.Versioning.ApiExplorer;
using HealthChecks.UI.Client;
using PeruGroup.Ecommerce.Application.Main.Extensiones;
using PeruGroup.Ecommerce.Infrastructure;
using PeruGroup.Ecommerce.Infrastructure.Repository.Extensiones;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.Authentication;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.Feature;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.GlobalException;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.HealthCheck;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.Middleware;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.RateLimiter;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.Redis;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.Swagger;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.Versioning;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.Watch;
using PeruGroup.Ecommerce.Services.WebApi.Helpers;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.Configure<AppSettings>(configuration.GetSection("Config")); // CONFIGURACION DE APPSETTINGS


builder.Services.AddFeature(configuration);//CORS

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SERVICIOS INJECTIONS
builder.Services.AddInfrastructureServices(); //SERVICIOS DE INFRAESTRUCTURA (EVENT BUS, MASSTRANSIT, RABBITMQ)
builder.Services.AddApplication(); //APLICACION
builder.Services.AddInfrastructure(configuration); //INFRAESTRUCTURA
builder.Services.AddGlobalException(); //MIDDLEWARE DE EXCEPCIONES

builder.Services.AddAutenticacion(builder.Configuration); //AUTENTICACION
builder.Services.AddVersioning(); //VERSIONADO DE API
builder.Services.AddSwagger(); //SWAGGER
builder.Services.AddHealthChecks(configuration); //HEALTH CHECKS
builder.Services.AddWatchDog(configuration); //WATCHDOG
builder.Services.AddRedisCache(configuration); //REDIS CACHE
builder.Services.AddRateLimiting(configuration); //RATE LIMITER

var app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

#region ENV_AMBIENTE_DE_DESARROLLO
// PARA DESARROLLO
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        foreach (var item in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.GroupName.ToUpperInvariant());
        }
    });
}
#endregion

#region ENV_AMBIENTE_DE_PRODUCCION
if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        foreach (var item in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.GroupName.ToUpperInvariant());
        }
    });

}
#endregion


app.UseReDoc(options =>
{
    foreach (var item in provider.ApiVersionDescriptions)
    {
        options.DocumentTitle = "Peru Group Ecommerce API";
        options.SpecUrl = $"/swagger/{item.GroupName}/swagger.json";
    }
});


app.UseWatchDogExceptionLogger(); //USO DE WATCHDOG
app.UseCors("policyApiEcommerce"); //USO DE CORS
app.UseAuthentication(); //USO DE AUTENTICACION
app.UseAuthorization(); //USO DE AUTORIZACION


if (!app.Environment.IsDevelopment()) //SOLO PARA DOCKER
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseRateLimiter(); //USO DE RATE LIMITER 
app.MapControllers();
app.MapHealthChecksUI(); //MAPEO DE HEALTH CHECKS UI
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.AddMiddleware(); //USO DE MIDDLEWARE PERSONALIZADO

app.UseWatchDog(conf =>
{
    conf.WatchPageUsername = configuration["WatchDog:WatchPageUsername"];
    conf.WatchPagePassword = configuration["WatchDog:WatchPagePassword"];
});

app.Run();
