using Exercises.Core.Entities;
using Exercises.Infrastructure.Repositories;
using Moq;

namespace Exercises.UnitTests.Mocks
{
    public static class MockExerciseRepository
    {
        public static Mock<IExerciseRepository> GetExerciseRepository()
        {
            var exercises = new List<Exercise> 
            { 
                new Exercise
                {
                    Id = 1,
                    Name = "Deadlift",
                },
                new Exercise
                {
                    Id = 2,
                    Name = "Squat",
                }
            };

            var mockRepo = new Mock<IExerciseRepository>();

            mockRepo.Setup(x => x.GetAllExercisesAsync()).ReturnsAsync(exercises);

            mockRepo.Setup(x => x.GetExerciseByIdAsync(1)).
                ReturnsAsync(exercises.FirstOrDefault(q => q.Id == 1));

            mockRepo.Setup(x => x.GetExercisesByNameAsync("Deadlift"))
                .ReturnsAsync(exercises.Where(q => q.Name.Contains("Deadlift")));

            return mockRepo;
        }
    }
}
