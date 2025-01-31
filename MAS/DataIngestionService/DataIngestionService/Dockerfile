FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DataIngestionService/DataIngestionService.csproj", "DataIngestionService/"]
RUN dotnet restore "DataIngestionService/DataIngestionService.csproj"
COPY . .
WORKDIR "/src/DataIngestionService"

RUN dotnet publish "DataIngestionService.csproj" -c Debug -o /app/publish

# Стадия 2: Создание рабочего образа
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
EXPOSE 8081

CMD ["dotnet", "DataIngestionService.dll"]