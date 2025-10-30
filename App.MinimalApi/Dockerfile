# 1. Build aþamasý
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "App.MinimalApi.csproj"
RUN dotnet publish "App.MinimalApi.csproj" -c Release -o /app/publish

# 2. Run aþamasý
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "App.MinimalApi.dll"]