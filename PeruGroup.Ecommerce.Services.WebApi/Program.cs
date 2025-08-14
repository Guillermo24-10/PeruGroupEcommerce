using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using PeruGroup.Ecommerce.Application.Main.Extensiones;
using PeruGroup.Ecommerce.Domain.Core.Extensiones;
using PeruGroup.Ecommerce.Infrastructure.Repository.Extensiones;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.Authentication;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.HealthCheck;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.Redis;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.Swagger;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.Versioning;
using PeruGroup.Ecommerce.Services.WebApi.Extensiones.Watch;
using PeruGroup.Ecommerce.Services.WebApi.Helpers;
using PeruGroup.Ecommerce.Transversal.Logging.Extensiones;
using PeruGroup.Ecommerce.Transversal.Mapper.Extensiones;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
//CORS
var policy = "policyApiEcommerce";
builder.Services.AddCors(options =>
{
    options.AddPolicy(policy,
        builder => builder.WithOrigins(configuration["Config:OriginCors"]!)
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.Configure<AppSettings>(configuration.GetSection("Config"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SERVICIOS INJECTIONS
builder.Services.AddApplication(); //APLICACION
builder.Services.AddInfrastructure(); //INFRAESTRUCTURA
builder.Services.AddDomain(); //DOMINIO
builder.Services.AddMappers(); //MAPPER
builder.Services.AddLogger(); //LOGGER

builder.Services.AddAutenticacion(builder.Configuration); //AUTENTICACION
builder.Services.AddVersioning(); //VERSIONADO DE API
builder.Services.AddSwagger(); //SWAGGER
builder.Services.AddHealthChecks(configuration); //HEALTH CHECKS
builder.Services.AddWatchDog(configuration); //WATCHDOG
builder.Services.AddRedisCache(configuration); //REDIS CACHE

var app = builder.Build();

#region CON_AMBIENTE_DE_DESARROLLO
// PARA DESARROLLO
if (app.Environment.IsDevelopment())
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
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

#region CON_AMBIENTE_DE_PRODUCCION
//var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    foreach (var item in provider.ApiVersionDescriptions)
//    {
//        c.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.GroupName.ToUpperInvariant());
//    }
//});
#endregion


app.UseWatchDogExceptionLogger(); //USO DE WATCHDOG
app.UseCors(policy); //USO DE CORS
app.UseAuthentication(); //USO DE AUTENTICACION
app.UseAuthorization(); //USO DE AUTORIZACION

if (!app.Environment.IsDevelopment()) //SOLO PARA DOCKER
{
    app.UseHttpsRedirection();
}

app.MapControllers();
app.MapHealthChecksUI(); //MAPEO DE HEALTH CHECKS UI
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseWatchDog(conf =>
{
    conf.WatchPageUsername = configuration["WatchDog:WatchPageUsername"];
    conf.WatchPagePassword = configuration["WatchDog:WatchPagePassword"];
});

app.Run();
