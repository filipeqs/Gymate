using AutoFixture.Xunit2;
using Exercises.Core.Entities;
using Exercises.FunctionalTests.Helpers;
using System.Text;
using System.Text.Json;

namespace Exercises.FunctionalTests.Controllers;

[Collection("Test Collection")]
public class CreateExerciseControllerTests : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDatabase;

    public CreateExerciseControllerTests(ExerciseApiFactory apiFactory)
    {
        _client = apiFactory.HttpClient;
        _resetDatabase = apiFactory.ResetDatabaseAsync;
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public async Task DisposeAsync() => await _resetDatabase();

    [Theory, AutoData]
    public async Task Create_Exercise_Should_Return_Success_Response(Exercise exercise)
    {
        var content = new StringContent(JsonSerializer.Serialize(exercise), UTF8Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(ExerciseRoutes.Post.CreateExercise, content);

        response.EnsureSuccessStatusCode();
    }
}
