# Указываем имя проекта через аргумент
ARG PROJECT=DevelopmentSucks.API

# Этап 1: Сборка проекта
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG PROJECT
WORKDIR /src

# Копируем .csproj отдельно — важно для кэширования
COPY ${PROJECT}/${PROJECT}.csproj ${PROJECT}/
RUN dotnet restore ${PROJECT}/${PROJECT}.csproj

# Копируем всё остальное
COPY . .
RUN dotnet publish ${PROJECT}/${PROJECT}.csproj -c Release -o /app/publish

# Этап 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
ARG PROJECT
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

# Команда запуска по умолчанию — API
CMD ["dotnet", "DevelopmentSucks.API.dll"]
