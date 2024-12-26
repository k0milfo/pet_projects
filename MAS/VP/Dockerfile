# Стадия сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем файл проекта и восстанавливаем зависимости
COPY VP/VP.csproj VP/VP.csproj
RUN dotnet restore VP/VP.csproj --verbosity detailed

# Копируем оставшиеся файлы и собираем проект
COPY . .
RUN dotnet build VP/VP.csproj -c Release --verbosity detailed

# Публикуем проект
RUN dotnet publish VP/VP.csproj -c Release -o /app/publish --verbosity detailed

# Стадия runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0-windowsservercore-ltsc2022 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Запускаем приложение
ENTRYPOINT ["dotnet", "VP.dll"]