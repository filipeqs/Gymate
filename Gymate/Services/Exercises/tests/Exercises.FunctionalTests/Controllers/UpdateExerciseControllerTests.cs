using Exercises.Core.Entities;
using Exercises.FunctionalTests.Helpers;
using Exercises.Infrastructure.Data;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Exercises.FunctionalTests.Controllers;

[Collection("Test Collection")]
public class UpdateExerciseControllerTests : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDatabase;
    private readonly ExerciseContext _context;

    public UpdateExerciseControllerTests(ExerciseApiFactory apiFactory)
    {
        _client = apiFactory.HttpClient;
        _resetDatabase = apiFactory.ResetDatabaseAsync;
        _context = apiFactory.Services.GetService<ExerciseContext>();
    }

    public async Task InitializeAsync() => await DatabaseHelpers.AddExercises(_context);
    public async Task DisposeAsync() =>  await _resetDatabase();

    [Fact]
    public async Task Update_Exercise_Should_Return_Success_Response()
    {
        var exerciseToUpdate = _context.Exercises.FirstOrDefault();
        exerciseToUpdate.Name = "Udpated";
        var content = new StringContent(JsonSerializer.Serialize(exerciseToUpdate), UTF8Encoding.UTF8, "application/json");
        var response = await _client.PutAsync(ExerciseRoutes.Put.UpdateExercise, content);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Update_Unexisting_Exercise_Should_Return_NotFound()
    {
        var exerciseToUpdate = new Exercise
        {
            Id = 99,
            Name = "Does not exists",
        };
        var content = new StringContent(JsonSerializer.Serialize(exerciseToUpdate), UTF8Encoding.UTF8, "application/json");
        var response = await _client.PutAsync(ExerciseRoutes.Put.UpdateExercise, content);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
