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
using JobBoard.Infrastructure.Services;
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
        policy.WithOrigins("http://localhost:3000", "https://a86a-217-19-215-68.ngrok-free.app")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

// Регистрация сервисов
builder.Services.AddScoped<IEmployerService, EmployerService>();
builder.Services.AddScoped<IVacancyService, VacancyService>();
builder.Services.AddScoped<IApplicantService, ApplicantService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailConfirmationService, EmailConfirmationService>();
builder.Services.AddScoped<IEmailConfirmationTokenRepository, EmailConfirmationTokenRepository>();

// Регистрация репозиториев
builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();
builder.Services.AddScoped<IEmployerRepository, EmployerRepository>();
builder.Services.AddScoped<IVacancyRepository, VacancyRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

// Регистрация AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Регистрация валидаторов
builder.Services.AddValidatorsFromAssemblyContaining<EmployerDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ConfirmEmailDtoValidator>();

// Регистрация PasswordHasher
builder.Services.AddSingleton<IPasswordHasher<Employer>, PasswordHasher<Employer>>();
builder.Services.AddSingleton<IPasswordHasher<Applicant>, PasswordHasher<Applicant>>();

// Регистрация контроллеров
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapGet("/", () => Results.Redirect("/swagger/index.html"));
}

app.Run();