using Gymate.Aggregator.Config;
using Gymate.Aggregator.Infrastructure;
using Gymate.Aggregator.Interfaces;
using Gymate.Aggregator.Services;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gymate.Aggregator", Version = "v1" });
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

builder.Services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication("Bearer")
.AddIdentityServerAuthentication("Bearer", options =>
{
    options.ApiName = "GymateAggregator";
    options.Authority = builder.Configuration.GetValue<string>("IdentityServerApi");
    options.RequireHttpsMetadata = false;
});

builder.Services.AddHttpClient<IExerciseService, ExerciseService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Urls:Exercise"]);
});

builder.Services.AddHttpClient<IWorkoutService, WorkoutService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Urls:Workout"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gymate.Aggregator v1"));
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
