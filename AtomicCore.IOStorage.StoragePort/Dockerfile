#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AtomicCore.IOStorage.StoragePort/AtomicCore.IOStorage.StoragePort.csproj", "AtomicCore.IOStorage.StoragePort/"]
COPY ["AtomicCore/AtomicCore.csproj", "AtomicCore/"]
COPY ["AtomicCore.IOStorage.Core/AtomicCore.IOStorage.Core.csproj", "AtomicCore.IOStorage.Core/"]
RUN dotnet restore "AtomicCore.IOStorage.StoragePort/AtomicCore.IOStorage.StoragePort.csproj"
COPY . .
WORKDIR "/src/AtomicCore.IOStorage.StoragePort"
RUN dotnet build "AtomicCore.IOStorage.StoragePort.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AtomicCore.IOStorage.StoragePort.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AtomicCore.IOStorage.StoragePort.dll"]