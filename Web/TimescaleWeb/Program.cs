using DataReaders.Extensions;
using ExceptionHandlers.Extensions;
using Microsoft.OpenApi.Models;
using Persistence.Extensions;
using TimescaleApplication.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title   = "TimescaleWeb API",
        Version = "v1"
    });
    c.OperationFilter<FileUploadFilter>();
});

var connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING__POSTGRES");

Console.WriteLine(connectionString);

builder.Services.AddExceptionHandlers();
builder.Services.AddDataReaders();
builder.Services.AddApplicationServices();
builder.Services.AddPersistence(connectionString ?? throw new ArgumentNullException(nameof(connectionString)));

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TimescaleWeb API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();