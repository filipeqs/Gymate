using Gymate.Aggregator.Interfaces;
using Gymate.Aggregator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IExerciseService, ExerciseService>(client => 
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:ExerciseUrl"]);
});

builder.Services.AddHttpClient<IWorkoutService, WorkoutService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:WorkoutUrl"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
