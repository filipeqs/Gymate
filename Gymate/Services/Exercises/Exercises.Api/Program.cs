using ExceptionHandling.Extensions;
using ExceptionHandling.Middleware;
using Exercises.Api.Extensions;
using Exercises.Domain;
using Exercises.Infrastructure;
using Exercises.Infrastructure.Data;
using MassTransit;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Exercise.API", Version = "v1" });
});

builder.Services.AddAuthentication("Bearer")
.AddIdentityServerAuthentication("Bearer", options =>
{
    options.ApiName = "ExercisesAPI";
    options.Authority = builder.Configuration.GetValue<string>("IdentityServerApi");
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

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
