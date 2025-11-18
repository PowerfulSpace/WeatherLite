📌 WeatherLite Web API
WeatherLite Web — это демонстрационный проект на ASP.NET Core, реализующий простой REST API для работы с прогнозом погоды и сервисом сокращения ссылок.

✨ Основные возможности:
Прогноз погоды

GET /WeatherForecast — возвращает список из 5 случайных прогнозов.

GET /WeatherForecast/one — возвращает один прогноз.

GET /WeatherForecast/three — возвращает три прогноза.

Сервис сокращения ссылок

POST /WeatherForecast/shorten — принимает длинный URL и возвращает сокращённую ссылку.

GET /WeatherForecast/expand?shortUrl=... — разворачивает сокращённую ссылку обратно в оригинальный URL.

GET /r/{code} — выполняет редирект по короткому коду на исходный адрес.

⚙️ Технологии:
ASP.NET Core (Microsoft.AspNetCore.Mvc) — для построения REST API.

Dependency Injection — внедрение сервисов (ILogger, ILinkShortenerService, IOptions).

Options pattern — для конфигурации базового URL сокращателя.

Random.Shared — генерация случайных температур и описаний погоды.

🎯 Назначение:
Проект служит учебным примером для:

демонстрации работы контроллеров в ASP.NET Core,

реализации простого сервиса сокращения ссылок,

генерации тестовых данных (прогнозов погоды).
