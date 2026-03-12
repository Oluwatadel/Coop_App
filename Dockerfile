# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY src/CoopApplication.Application/CoopApplication.api.csproj src/CoopApplication.Application/
COPY src/CoopApplication.Domain/CoopApplication.Domain.csproj src/CoopApplication.Domain/
COPY src/CoopApplication.Persistence/CoopApplication.Persistence.csproj src/CoopApplication.Persistence/
COPY src/CoopApplication.Services/CoopApplication.Services.csproj src/CoopApplication.Services/
COPY src/CoopApplication.Application/CoopApplication.sln src/CoopApplication.Application/
RUN dotnet restore src/CoopApplication.Application/CoopApplication.api.csproj

COPY . .
RUN dotnet publish src/CoopApplication.Application/CoopApplication.api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CoopApplication.api.dll"]
