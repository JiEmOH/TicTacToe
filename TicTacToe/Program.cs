using Microsoft.EntityFrameworkCore;
using TicTacToe.Data;
using TicTacToe.Services;

var builder = WebApplication.CreateBuilder(args);

// DbContext (���� �����������)
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql("Host=localhost;Username=myuser;Password=mypassword;Database=TicTacToe"));

// Singleton � ����� ��������� ���� ����������� � �������
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

// ����� ��������, ���� ����� HTTPS ��������
// app.UseHttpsRedirection();

// ���� �� ����������� ����������� � ����� �� ��������
app.UseAuthorization();

app.MapControllers();

app.Run();
