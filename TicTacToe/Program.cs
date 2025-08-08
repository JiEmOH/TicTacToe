using Microsoft.EntityFrameworkCore;
using TicTacToe.Data;
using TicTacToe.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавляем контекст базы данных (PostgreSQL)
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql("Host=localhost;Username=myuser;Password=mypassword;Database=TicTacToe"));

// Регистрируем сервис логики игры (обязательно)
builder.Services.AddScoped<GameLogicService>();

// Добавляем контроллеры
builder.Services.AddControllers();

// Swagger — генерация OpenAPI-документации
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS — политика для всего
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Настраиваем адреса для прослушивания (0.0.0.0:5000)
app.Urls.Add("http://0.0.0.0:5000");

// Включаем CORS
app.UseCors("AllowAll");

// Включаем Swagger UI на корне (/)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicTacToe API V1");
    c.RoutePrefix = string.Empty;
});

// Авторизация, если нужна
app.UseAuthorization();

// Маршрутизация для контроллеров
app.MapControllers();

// Запускаем приложение
app.Run();
