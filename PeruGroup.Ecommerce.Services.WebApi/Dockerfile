# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENV DOTNET_ENVIRONMENT=Development


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PeruGroup.Ecommerce.Services.WebApi/PeruGroup.Ecommerce.Services.WebApi.csproj", "PeruGroup.Ecommerce.Services.WebApi/"]
COPY ["PeruGroup.Ecommerce.Application.Interface/PeruGroup.Ecommerce.Application.Interface.csproj", "PeruGroup.Ecommerce.Application.Interface/"]
COPY ["PeruGroup.Ecommerce.Application.DTO/PeruGroup.Ecommerce.Application.DTO.csproj", "PeruGroup.Ecommerce.Application.DTO/"]
COPY ["PeruGroup.Ecommerce.Transversal.Commons/PeruGroup.Ecommerce.Transversal.Commons.csproj", "PeruGroup.Ecommerce.Transversal.Commons/"]
COPY ["PeruGroup.Ecommerce.Application.Main/PeruGroup.Ecommerce.Application.Main.csproj", "PeruGroup.Ecommerce.Application.Main/"]
COPY ["PeruGroup.Ecommerce.Domain.Entity/PeruGroup.Ecommerce.Domain.Entity.csproj", "PeruGroup.Ecommerce.Domain.Entity/"]
COPY ["PeruGroup.Ecommerce.Domain.Interface/PeruGroup.Ecommerce.Domain.Interface.csproj", "PeruGroup.Ecommerce.Domain.Interface/"]
COPY ["PeruGroup.Ecommerce.Domain.Core/PeruGroup.Ecommerce.Domain.Core.csproj", "PeruGroup.Ecommerce.Domain.Core/"]
COPY ["PeruGroup.Ecommerce.Infrastructure.Interface/PeruGroup.Ecommerce.Infrastructure.Interface.csproj", "PeruGroup.Ecommerce.Infrastructure.Interface/"]
COPY ["PeruGroup.Ecommerce.Infrastructure.Repository/PeruGroup.Ecommerce.Infrastructure.Repository.csproj", "PeruGroup.Ecommerce.Infrastructure.Repository/"]
COPY ["PeruGroup.Ecommerce.Infrastructure.Data/PeruGroup.Ecommerce.Infrastructure.Data.csproj", "PeruGroup.Ecommerce.Infrastructure.Data/"]
COPY ["PeruGroup.Ecommerce.Transversal.Logging/PeruGroup.Ecommerce.Transversal.Logging.csproj", "PeruGroup.Ecommerce.Transversal.Logging/"]
COPY ["PeruGroup.Ecommerce.Transversal.Mapper/PeruGroup.Ecommerce.Transversal.Mapper.csproj", "PeruGroup.Ecommerce.Transversal.Mapper/"]
RUN dotnet restore "./PeruGroup.Ecommerce.Services.WebApi/PeruGroup.Ecommerce.Services.WebApi.csproj"
COPY . .
WORKDIR "/src/PeruGroup.Ecommerce.Services.WebApi"
RUN dotnet build "./PeruGroup.Ecommerce.Services.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PeruGroup.Ecommerce.Services.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PeruGroup.Ecommerce.Services.WebApi.dll"]