using System;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ProjectManagerApi.Helpers;
using ProjectManagerApi.Services;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    services.AddDbContext<DataContext>();
    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // configure DI for application services
    services.AddScoped<IProjectService, ProjectService>();
    services.AddScoped<ITaskService, TaskService>();
    services.AddScoped<IHistoryService, HistoryService>();
}

var app = builder.Build();

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
await dbContext.Database.MigrateAsync();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapControllers();
}

app.Run("http://localhost:4000");