# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore "AtomicCore.WebService.RequestAgent.csproj"

# Copy everything else and build
COPY . ./
RUN dotnet publish "AtomicCore.WebService.RequestAgent.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
ENV Logging__Console__FormatterName=simple
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "AtomicCore.WebService.RequestAgent.dll"]