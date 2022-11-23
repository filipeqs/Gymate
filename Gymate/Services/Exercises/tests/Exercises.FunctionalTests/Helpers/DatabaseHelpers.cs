using Exercises.Core.Entities;
using Exercises.Infrastructure.Data;

namespace Exercises.FunctionalTests.Helpers
{
    public static class DatabaseHelpers
    {
        private static IEnumerable<Exercise> GetExercises()
        {
            var exercises = new List<Exercise>
            {
                new Exercise
                {
                    Name = "Squat"
                },
                new Exercise
                {
                    Name = "Leg Press"
                },
                new Exercise
                {
                    Name = "Bench Press"
                },
                new Exercise
                {
                    Name = "Deadlift"
                },
                new Exercise
                {
                    Name = "Calf Raise"
                },
            };

            return exercises;
        }

        public static async Task AddExercises(ExerciseContext context)
        {
            context.Exercises.AddRange(GetExercises());
            await context.SaveChangesAsync();
        }
    }
}
