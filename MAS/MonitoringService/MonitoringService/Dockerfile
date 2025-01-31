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