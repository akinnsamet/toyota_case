FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Api/Toyota.Web/Toyota.Web.csproj", "Api/Toyota.Web/"]
RUN dotnet restore "./Api/Toyota.Web/Toyota.Web.csproj"
COPY . .
WORKDIR "/src/Api/Toyota.Web"
RUN dotnet build "./Toyota.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Toyota.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Toyota.Web.dll"]
