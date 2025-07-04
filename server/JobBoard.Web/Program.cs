using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using JobBoard.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Чтение строки подключения
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Строка подключения 'DefaultConnection' не найдена.");

// Регистрация DbContext
builder.Services.AddDbContext<JobBoardDbContext>(options =>
    options.UseNpgsql(connectionString)
           .EnableSensitiveDataLogging(builder.Environment.IsDevelopment()));

// Настройка CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
        policy.WithOrigins("http://localhost:3000", "https://032a-80-94-250-53.ngrok-free.app")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Регистрация контроллеров
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Регистрация Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobBoard API", Version = "v1" });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobBoard API v1"));
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();

// Перенаправление с корневого URL на Swagger в режиме разработки
if (app.Environment.IsDevelopment())
{
    app.MapGet("/", () => Results.Redirect("/swagger/index.html"));
}

app.Run();