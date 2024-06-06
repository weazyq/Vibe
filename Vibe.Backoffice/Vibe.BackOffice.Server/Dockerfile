#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM node:21.7.3-alpine3.20 AS nodebuild
WORKDIR /src

# Copy package.json and package-lock.json for frontend dependencies
COPY Vibe.BackOffice/vibe.backoffice.client/package*.json ./Vibe.BackOffice/vibe.backoffice.client/
RUN cd Vibe.BackOffice/vibe.backoffice.client && npm install

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_16.x | bash -
RUN apt-get install -y nodejs

COPY . .

COPY ["Vibe.BackOffice/vibe.backoffice.client/nuget.config", "Vibe.BackOffice/vibe.backoffice.client/"]
COPY ["Vibe.BackOffice/Vibe.BackOffice.Server/Vibe.BackOffice.Server.csproj", "Vibe.BackOffice/Vibe.BackOffice.Server/"]
COPY ["Vibe.Configurator/Vibe.Configurator.csproj", "Vibe.Configurator/"]
COPY ["Vibe.Domain/Vibe.Domain.csproj", "Vibe.Domain/"]
COPY ["Vibe.Tools/Vibe.Tools.csproj", "Vibe.Tools/"]
COPY ["Vibe.EF/Vibe.EF.csproj", "Vibe.EF/"]
COPY ["Vibe.ServiceDefaults/Vibe.ServiceDefaults.csproj", "Vibe.ServiceDefaults/"]
COPY ["Vibe.Services/Vibe.Services.csproj", "Vibe.Services/"]
COPY ["Vibe.BackOffice/vibe.backoffice.client/vibe.backoffice.client.esproj", "Vibe.BackOffice/vibe.backoffice.client/"]

RUN dotnet restore "./Vibe.BackOffice/Vibe.BackOffice.Server/Vibe.BackOffice.Server.csproj"

WORKDIR "/src/Vibe.BackOffice/Vibe.BackOffice.Server"
RUN dotnet build "./Vibe.BackOffice.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/Vibe.BackOffice/Vibe.BackOffice.Server"
RUN dotnet publish "./Vibe.BackOffice.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vibe.BackOffice.Server.dll"]