using AutoFixture.Xunit2;
using Exercises.Core.Entities;
using FluentAssertions;
using System.Text.Json;
using System.Net;
using System.Text;

namespace Exercises.FunctionalTests.Controllers
{
    public class ExercisesControllerTests : ExercisesTestBase
    {
        [Fact]
        public async Task Get_All_Exercises_Should_Return_Exercises_and_Success_Response()
        {
            using var server = CreateServer();
            var response = await server.CreateClient().GetAsync(Get.Exercises);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_Exercises_ById_Should_Return_Exercise_and_Success_Response()
        {
            using var server = CreateServer();
            var response = await server.CreateClient().GetAsync($"{Get.ExerciseById}/1");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_Unexisting_Exercise_ById_Should_Return_NotFound()
        {
            using var server = CreateServer();
            var response = await server.CreateClient().GetAsync($"{Get.ExerciseById}/99");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Exercises_ByName_Should_Return_Exercises_and_Ok_Response()
        {
            using var server = CreateServer();
            var response = await server.CreateClient().GetAsync($"{Get.ExercisesByName}/Press");

            response.EnsureSuccessStatusCode();
        }

        [Theory, AutoData]
        public async Task Create_Exercise_Should_Return_Success_Response(Exercise exercise)
        {
            using var server = CreateServer();
            var content = new StringContent(JsonSerializer.Serialize(exercise), UTF8Encoding.UTF8, "application/json");
            var response = await server.CreateClient().PostAsync(Post.CreateExercise, content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_Exercise_Should_Return_Success_Response()
        {
            using var server = CreateServer();
            var exerciseToUpdate = new Exercise
            {
                Id = 1,
                Name = "Updated",
            };
            var content = new StringContent(JsonSerializer.Serialize(exerciseToUpdate), UTF8Encoding.UTF8, "application/json");
            var response = await server.CreateClient().PutAsync(Put.UpdateExercise, content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_Unexisting_Exercise_Should_Return_NotFound()
        {
            using var server = CreateServer();
            var exerciseToUpdate = new Exercise
            {
                Id = 99,
                Name = "Does not exists",
            };
            var content = new StringContent(JsonSerializer.Serialize(exerciseToUpdate), UTF8Encoding.UTF8, "application/json");
            var response = await server.CreateClient().PutAsync(Put.UpdateExercise, content);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_Exercise_Should_Return_SuccessResponse()
        {
            using var server = CreateServer();
            var response = await server.CreateClient().DeleteAsync($"{Delete.DeleteExercise}/1");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Delete_Unexisting_Exercise_Should_Return_NotFound()
        {
            using var server = CreateServer();
            var response = await server.CreateClient().DeleteAsync($"{Delete.DeleteExercise}/99");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
