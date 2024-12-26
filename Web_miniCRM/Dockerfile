FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY out/ ./
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet", "Web_miniCRM.dll"]
