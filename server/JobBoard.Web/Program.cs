
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using JobBoard.Infrastructure.Data;
using JobBoard.Infrastructure.Data.Repositories;
using JobBoard.Application.Services;
using FluentValidation;
using JobBoard.Application.Validators;
using System.Text.Json.Serialization;
using AutoMapper;
using JobBoard.Application.Mappers;
using JobBoard.Core.Entities;
using JobBoard.Core.Interfaces;
using Microsoft.AspNetCore.Identity;


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
              .AllowAnyMethod()
              .AllowCredentials());
});

builder.Services.AddSingleton<IPasswordHasher<Employer>, PasswordHasher<Employer>>();
builder.Services.AddSingleton<IPasswordHasher<Applicant>, PasswordHasher<Applicant>>();
// Регистрация AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Регистрация контроллеров
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Сериализация перечислений как строк
    });

// Регистрация Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobBoard API", Version = "v1" });
});

// Регистрация сервисов
builder.Services.AddScoped<IEmployerService, EmployerService>();
builder.Services.AddScoped<IVacancyService, VacancyService>();
builder.Services.AddScoped<IApplicantService, ApplicantService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddValidatorsFromAssemblyContaining<EmployerDtoValidator>();

// Регистрация репозиториев
builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();
builder.Services.AddScoped<IEmployerRepository, EmployerRepository>();
builder.Services.AddScoped<IVacancyRepository, VacancyRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IEmailConfirmationTokenRepository, EmailConfirmationTokenRepository>();

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

if (app.Environment.IsDevelopment())
{
    app.MapGet("/", () => Results.Redirect("/swagger/index.html"));
}

app.Run();