namespace Exercises.FunctionalTests.Repository
{
    public class ExerciseRepositoryTests : ExercisesTestBase
    {
        [Fact]
        public void Exercise()
        {
            // Set env to Dev
            using var server = CreateServer();
        }
    }
}
