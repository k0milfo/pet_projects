<<<<<<< HEAD
# Стадия 1: Сборка образа с инструментами для установки EF и публикации
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MonitoringService/MonitoringService.csproj", "MonitoringService/"]
RUN dotnet restore "MonitoringService/MonitoringService.csproj"
COPY . .
WORKDIR "/src/MonitoringService"


#TEST
# Установка EF (если она необходима)
#RUN dotnet tool install --global dotnet-ef
#ENV PATH="$PATH:/root/.dotnet/tools"
#ENV ASPNETCORE_ENVIRONMENT=Development

# Создаем новую миграцию
# RUN dotnet ef migrations add NewMigration
# Выполнение миграций (если необходимы)
# RUN dotnet ef database update

RUN dotnet publish "MonitoringService.csproj" -c Debug -o /app/publish

# Стадия 2: Создание рабочего образа
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

#TEST
# Установка mssql-tools и зависимостей
#RUN apt-get update && apt-get install -y curl apt-transport-https gnupg && \
#    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
#    curl https://packages.microsoft.com/config/debian/10/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
#    apt-get update && ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev && \
#    echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc && \
#    echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.profile && \
#    apt-get clean && rm -rf /var/lib/apt/lists/*

EXPOSE 8080
EXPOSE 8081

CMD ["dotnet", "MonitoringService.dll"]
=======
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Monitoring Service/Monitoring Service.csproj", "Monitoring Service/"]
RUN dotnet restore "./Monitoring Service/Monitoring Service.csproj"
COPY . .
WORKDIR "/src/Monitoring Service"
RUN dotnet build "./Monitoring Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Monitoring Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Monitoring Service.dll"]
>>>>>>> 7a9adf1f8c04b7d7e362b4edabbc562e1a0f96d9
