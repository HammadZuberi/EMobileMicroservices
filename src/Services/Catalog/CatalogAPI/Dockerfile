#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

#Store and reference both of the Application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Catalog/CatalogAPI/CatalogAPI.csproj", "Services/Catalog/CatalogAPI/"]
#dependencies
COPY ["BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/BuildingBlocks/"]
RUN dotnet restore "./Services/Catalog/CatalogAPI/CatalogAPI.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/CatalogAPI"
RUN dotnet build "./CatalogAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# publish refrence form base Build 
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CatalogAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#copy publish endpooint
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogAPI.dll"]