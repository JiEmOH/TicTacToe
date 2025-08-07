using Microsoft.EntityFrameworkCore;
using TicTacToe.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseNpgsql("Host=localhost;Username=myuser;Password=mypassword;Database=TicTacToe")
);
// Add services to the container.

builder.Services.AddControllers();

// Логика игры
builder.Services.AddScoped<GameLogicService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();
app.Urls.Add("http://0.0.0.0:5000");

// CORS
app.UseCors("AllowAll");

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicTacToe API v1");
    c.RoutePrefix = string.Empty; // Swagger будет по адресу /
});
app.UseAuthorization();

app.MapControllers();







app.Run();
