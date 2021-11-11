#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AtomicCore.WebService.RequestAgent/AtomicCore.WebService.RequestAgent.csproj", "AtomicCore.WebService.RequestAgent/"]
COPY ["AtomicCore/AtomicCore.csproj", "AtomicCore/"]
RUN dotnet restore "AtomicCore.WebService.RequestAgent/AtomicCore.WebService.RequestAgent.csproj"
COPY . .
WORKDIR "/src/AtomicCore.WebService.RequestAgent"
RUN dotnet build "AtomicCore.WebService.RequestAgent.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AtomicCore.WebService.RequestAgent.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AtomicCore.WebService.RequestAgent.dll"]