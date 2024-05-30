FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE  8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sale/Sale.Domain/Sale.Domain.csproj", "Sale.Domain/"]
COPY ["Sale/Sale.Repository/Sale.Repository.csproj", "Sale.Repository/"]
COPY ["Sale/Sale.Service/Sale.Service.csproj", "Sale.Service/"]


RUN dotnet restore 
