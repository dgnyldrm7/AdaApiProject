#Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

COPY ["App.MinimalApi/App.MinimalApi.csproj", "App.MinimalApi/"]
COPY ["App.Core/App.Core.csproj", "App.Core/"]
COPY ["App.Services/App.Services.csproj", "App.Services/"]

# Restore
RUN dotnet restore "App.MinimalApi/App.MinimalApi.csproj"

COPY . .

# Build ve publish
WORKDIR /source/App.MinimalApi
RUN dotnet publish -c Release -o /app/publish

# Run
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "App.MinimalApi.dll"]
