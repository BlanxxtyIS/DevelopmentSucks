# backend/Dockerfile

# Этап 1: Сборка проекта
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем .csproj отдельно для кэширования зависимостей
COPY DevelopmentSucks.API/*.csproj ./DevelopmentSucks.API/
RUN dotnet restore ./DevelopmentSucks.API/DevelopmentSucks.API.csproj

# Копируем всё и билдим
COPY . .
RUN dotnet publish DevelopmentSucks.API/DevelopmentSucks.API.csproj -c Release -o /app/publish

# Этап 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "DevelopmentSucks.API.dll"]
