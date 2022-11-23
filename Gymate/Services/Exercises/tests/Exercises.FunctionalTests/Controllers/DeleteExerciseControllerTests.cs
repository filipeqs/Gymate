using Exercises.FunctionalTests.Helpers;
using Exercises.Infrastructure.Data;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Exercises.FunctionalTests.Controllers;

[Collection("Test Collection")]
public class DeleteExerciseControllerTests : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDatabase;
    private readonly ExerciseContext _context;

    public DeleteExerciseControllerTests(ExerciseApiFactory apiFactory)
    {
        _client = apiFactory.HttpClient;
        _resetDatabase = apiFactory.ResetDatabaseAsync;
        _context = apiFactory.Services.GetService<ExerciseContext>();
    }

    public async Task InitializeAsync() => await DatabaseHelpers.AddExercises(_context);
    public async Task DisposeAsync() => await _resetDatabase();

    [Fact]
    public async Task Delete_Exercise_Should_Return_SuccessResponse()
    {
        var exerciseToDelete = _context.Exercises.FirstOrDefault();
        var response = await _client.DeleteAsync($"{ExerciseRoutes.Delete.DeleteExercise}/{exerciseToDelete.Id}");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Delete_Unexisting_Exercise_Should_Return_NotFound()
    {
        var response = await _client.DeleteAsync($"{ExerciseRoutes.Delete.DeleteExercise}/99");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
