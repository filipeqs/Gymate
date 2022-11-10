using EventBus.Messages.Common;
using MassTransit;
using Microsoft.OpenApi.Models;
using Workout.Api.EventBusConsumer;
using Workout.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
    options.Authority = builder.Configuration.GetValue<string>("IdentityServerApi");
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsLocal())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workout.API v1"));
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
