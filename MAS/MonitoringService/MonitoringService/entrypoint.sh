set -e
dotnet ef database update --no-build
dotnet MonitoringService.dll