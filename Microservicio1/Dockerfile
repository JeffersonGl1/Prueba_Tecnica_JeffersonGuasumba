#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base



USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
ENV DB_CONNECTION_STRING="Data Source = 192.168.100.69,1433;Initial Catalog=Prueba_Tecnica;TrustServerCertificate=True;User ID=sa;Password=adminadmin"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Microservicio1/Microservicio1.csproj", "Microservicio1/"]
RUN dotnet restore "./Microservicio1/Microservicio1.csproj"
COPY . .
WORKDIR "/src/Microservicio1"
RUN dotnet build "./Microservicio1.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Microservicio1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservicio1.dll"]