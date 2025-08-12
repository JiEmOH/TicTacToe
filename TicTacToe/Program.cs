using Microsoft.EntityFrameworkCore;
using TicTacToe.Data;
using TicTacToe.Services;

var builder = WebApplication.CreateBuilder(args);

// DbContext: InMemory by default for simpler dev, Npgsql when configured
var useInMemory = builder.Configuration.GetValue("UseInMemory", true);
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    if (useInMemory)
    {
        options.UseInMemoryDatabase("TicTacToeDb");
    }
    else
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

// Логику игры регистрируем без состояния
builder.Services.AddTransient<GameLogicService>();

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

// Для контейнера/разработки можно задавать через переменную ASPNETCORE_URLS
// app.Urls.Add("http://0.0.0.0:5000");

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicTacToe API V1");
    c.RoutePrefix = string.Empty;
});

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
