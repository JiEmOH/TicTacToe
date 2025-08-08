using Microsoft.EntityFrameworkCore;
using TicTacToe.Data;
using TicTacToe.Services;

var builder = WebApplication.CreateBuilder(args);

// DbContext (если понадобитс€)
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql("Host=localhost;Username=myuser;Password=mypassword;Database=TicTacToe"));

// Singleton Ч чтобы состо€ние игры сохран€лось в сервисе
builder.Services.AddSingleton<GameLogicService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.Urls.Add("http://0.0.0.0:5000");

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicTacToe API V1");
    c.RoutePrefix = string.Empty;
});

// ћожно добавить, если нужно HTTPS редирект
// app.UseHttpsRedirection();

// ≈сли не используешь авторизацию Ч можно не вызывать
app.UseAuthorization();

app.MapControllers();

app.Run();
