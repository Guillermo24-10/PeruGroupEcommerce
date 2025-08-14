using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PeruGroup.Ecommerce.Services.WebApi.Extensiones.Authentication
{
    public static class AutenticacionExtension
    {
        public static IServiceCollection AddAutenticacion(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration["Config:Secret"]!);
            var issuer = configuration["Config:Issuer"]!;
            var audience = configuration["Config:Audience"]!;

            _ = services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userId = int.Parse(context.Principal!.Identity!.Name!);
                        return Task.CompletedTask;
                    },

                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Append("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
            });

            return services;
        }
    }
}
