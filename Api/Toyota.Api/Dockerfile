FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Api/Toyota.Api/Toyota.Api.csproj", "Api/Toyota.Api/"]
COPY ["Api/Toyota.Business/Toyota.Business.csproj", "Api/Toyota.Business/"]
COPY ["Core/Toyota.Application/Toyota.Application.csproj", "Core/Toyota.Application/"]
COPY ["Core/Toyota.Entities/Toyota.Entities.csproj", "Core/Toyota.Entities/"]
COPY ["Shared/Toyota.Shared/Toyota.Shared.csproj", "Shared/Toyota.Shared/"]
COPY ["Infrastructure/Toyota.Data/Toyota.Data.csproj", "Infrastructure/Toyota.Data/"]
COPY ["Infrastructure/Toyota.Services/Toyota.Services.csproj", "Infrastructure/Toyota.Services/"]
RUN dotnet restore "./Api/Toyota.Api/Toyota.Api.csproj"

COPY . .
WORKDIR "/src/Api/Toyota.Api"
RUN dotnet build "./Toyota.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Toyota.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

USER root
COPY ["Api/Toyota.Api/Data/", "/app/Data/"]
RUN chown -R app:app /app/Data

USER app

ENTRYPOINT ["dotnet", "Toyota.Api.dll"]
