var builder = WebApplication.CreateBuilder(args);

// Разрешить CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowReactApp"); // Применить CORS
app.MapControllers();

app.Run();