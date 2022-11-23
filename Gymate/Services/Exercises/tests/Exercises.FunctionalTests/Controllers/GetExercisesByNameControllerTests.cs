using Exercises.FunctionalTests.Helpers;

namespace Exercises.FunctionalTests.Controllers;

[Collection("Test Collection")]
public class GetExercisesByNameControllerTests : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDatabase;

    public GetExercisesByNameControllerTests(ExerciseApiFactory apiFactory)
    {
        _client = apiFactory.HttpClient;
        _resetDatabase = apiFactory.ResetDatabaseAsync;
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public async Task DisposeAsync() => await _resetDatabase();

    [Fact]
    public async Task Get_Exercises_ByName_Should_Return_Exercises_and_Ok_Response()
    {
        var response = await _client.GetAsync($"{ExerciseRoutes.Get.ExercisesByName}/Press");

        response.EnsureSuccessStatusCode();
    }
}
