namespace Exercises.FunctionalTests.Controllers
{
    public class ExercisesControllerTests : ExercisesTestBase
    {
        [Fact]
        public async Task Get_All_Exercises_should_Return_Exercises_and_Ok_Response()
        {
            using var server = CreateServer();
            var response = await server.CreateClient().GetAsync(Get.Exercises);

            response.EnsureSuccessStatusCode();
        }
    }
}
