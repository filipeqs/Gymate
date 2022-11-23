using Exercises.FunctionalTests.Helpers;
using Exercises.Infrastructure.Data;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Exercises.FunctionalTests.Controllers;

[Collection("Test Collection")]
public class GetExerciseByIdControllerTests : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDatabase;
    private readonly ExerciseContext _context;

    public GetExerciseByIdControllerTests(ExerciseApiFactory apiFactory)
    {
        _client = apiFactory.HttpClient;
        _resetDatabase = apiFactory.ResetDatabaseAsync;
        _context = apiFactory.Services.GetService<ExerciseContext>();
    }

    public async Task InitializeAsync() => await DatabaseHelpers.AddExercises(_context);
    public async Task DisposeAsync() => await _resetDatabase();

    [Fact]
    public async Task Get_Exercises_ById_Should_Return_Exercise_and_Success_Response()
    {
        var exerciseToSearch = _context.Exercises.FirstOrDefault();
        var response = await _client.GetAsync($"{ExerciseRoutes.Get.ExerciseById}/{exerciseToSearch.Id}");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Get_Unexisting_Exercise_ById_Should_Return_NotFound()
    {
        var response = await _client.GetAsync($"{ExerciseRoutes.Get.ExerciseById}/99");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
