using Common.Logging;
using EventBus.Messages.Common;
using ExceptionHandling.Extensions;
using ExceptionHandling.Middleware;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;
using Workouts.Api.EventBusConsumer;
using Workouts.Api.Extensions;
using Workouts.Application;
using Workouts.Infrastructure;
using Workouts.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog(SeriLogger.Configure);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Workout.API", Version = "v1" });
});

builder.Services.AddScoped<ExerciseUpdateConsumer>();

builder.Services.AddAuthentication("Bearer")
.AddIdentityServerAuthentication("Bearer", options =>
{
    options.ApiName = "WourkoutAPI";
    options.Authority = builder.Configuration.GetValue<string>("Urls:IdentityServerApi");
    options.RequireHttpsMetadata = false;
});

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<ExerciseUpdateConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ReceiveEndpoint(EventBusConstants.ExerciseUpdateQueue, c =>
        {
            c.ConfigureConsumer<ExerciseUpdateConsumer>(ctx);
        });
    });
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddExceptionHandlingServices();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy())
    .AddSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        name: "workoutdb-check",
        tags: new string[] { "workoutdb" })
    .AddElasticsearch(
        builder.Configuration.GetConnectionString("ElasticSearchConnection"),
        name: "elasticsearch-check",
        tags: new string[] { "elasticsearch" });

var app = builder.Build();

app.MigrateDbContext<WorkoutContext>((context, services) =>
{
    var env = services.GetService<IHostEnvironment>();
    var logger = services.GetService<ILogger<WorkoutContextSeed>>();

    new WorkoutContextSeed()
        .SeedAsync(context, env, logger)
        .Wait();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsLocal())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workout.API v1"));
}

app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
