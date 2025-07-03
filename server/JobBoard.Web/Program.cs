var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowReact", policy =>
            policy.WithOrigins("http://localhost:3000","https://032a-80-94-250-53.ngrok-free.app")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

// CORS должен идти после UseRouting и до UseEndpoints
app.UseCors("AllowReact");

app.UseAuthorization(); // если есть

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();