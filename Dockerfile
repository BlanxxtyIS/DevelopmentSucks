# Базовый образ SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build 
WORKDIR /src

# Копируем всё в контейнер
COPY . .

# Восстановление зависимостей
RUN dotnet restore "DevelopmentSucks.API/DevelopmentSucks.API.csproj"

# Публикация
RUN dotnet publish "DevelopmentSucks.API/DevelopmentSucks.API.csproj" -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DevelopmentSucks.API.dll"]