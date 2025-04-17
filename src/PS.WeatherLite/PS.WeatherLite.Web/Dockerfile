# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Копируем csproj и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем всё остальное и билдим
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Копируем собранное приложение
COPY --from=build /app/out ./

# Указываем порт
EXPOSE 8080

# Команда запуска
ENTRYPOINT ["dotnet", "PS.WeatherLite.Web.dll"]