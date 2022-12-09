using Common.Logging;
using ExceptionHandling.Extensions;
using ExceptionHandling.Middleware;
using Exercises.Api.Extensions;
using Exercises.Domain;
using Exercises.Infrastructure;
using Exercises.Infrastructure.Data;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog(SeriLogger.Configure);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Exercise.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string> {}
        }
    });
});

builder.Services.AddAuthentication("Bearer")
.AddIdentityServerAuthentication("Bearer", options =>
{
    options.ApiName = "ExercisesAPI";
    options.Authority = builder.Configuration.GetValue<string>("Urls:IdentityServerApi");
    options.RequireHttpsMetadata = false;
});

builder.Services.AddExceptionHandlingServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy())
    .AddSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        name: "exercisedb-check",
        tags: new string[] { "exercisedb" })
    .AddRedis(
        builder.Configuration.GetConnectionString("RedisConnection"),
        name: "redis-check",
        tags: new string[] { "redis" })
    .AddElasticsearch(
        builder.Configuration.GetConnectionString("ElasticSearchConnection"),
        name: "elasticsearch-check",
        tags: new string[] { "elasticsearch" })
    .AddRabbitMQ(
        builder.Configuration["EventBusSettings:HostAddress"],
        name: "exercise-rabitmqbus-check",
        tags: new string[] { "rabbitmqbus" });

var app = builder.Build();

app.MigrateDbContext<ExerciseContext>((context, services) =>
{
    var env = services.GetService<IHostEnvironment>();
    var logger = services.GetService<ILogger<ExerciseContextSeed>>();

    new ExerciseContextSeed()
        .SeedAsync(context, env, logger)
        .Wait();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsLocal())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exercise.API v1"));
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
