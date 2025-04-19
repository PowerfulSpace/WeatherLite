using PS.WeatherLite.Web.Models;
using PS.WeatherLite.Web.Services.Interfaces;
using PS.WeatherLite.Web.Services;
using PS.WeatherLite.Web.Storage.Interfaces;
using PS.WeatherLite.Web.Storage;

var builder = WebApplication.CreateBuilder(args);


// Настройка конфигурации
builder.Services.Configure<ShortenerSettings>(builder.Configuration.GetSection("Shortener"));

// Регистрация Redis хранилища
string redisConnection = builder.Configuration.GetSection("Redis")["ConnectionString"]!;
builder.Services.AddSingleton<ILinkStorage>(new RedisLinkStorage(redisConnection));

// Регистрация сервиса сокращения ссылок
builder.Services.AddSingleton<ILinkShortenerService, LinkShortenerService>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapGet("/", () => Results.Redirect("/swagger"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();