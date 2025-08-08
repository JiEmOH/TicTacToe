using Microsoft.EntityFrameworkCore;
using TicTacToe.Data;
using TicTacToe.Services;

var builder = WebApplication.CreateBuilder(args);

// ��������� �������� ���� ������ (PostgreSQL)
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql("Host=localhost;Username=myuser;Password=mypassword;Database=TicTacToe"));

// ������������ ������ ������ ���� (�����������)
builder.Services.AddScoped<GameLogicService>();

// ��������� �����������
builder.Services.AddControllers();

// Swagger � ��������� OpenAPI-������������
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS � �������� ��� �����
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// ����������� ������ ��� ������������� (0.0.0.0:5000)
app.Urls.Add("http://0.0.0.0:5000");

// �������� CORS
app.UseCors("AllowAll");

// �������� Swagger UI �� ����� (/)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicTacToe API V1");
    c.RoutePrefix = string.Empty;
});

// �����������, ���� �����
app.UseAuthorization();

// ������������� ��� ������������
app.MapControllers();

// ��������� ����������
app.Run();
