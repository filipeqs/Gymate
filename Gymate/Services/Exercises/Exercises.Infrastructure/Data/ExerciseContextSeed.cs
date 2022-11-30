using Exercises.Core.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Exercises.Infrastructure.Data
{
    public class ExerciseContextSeed
    {
        public async Task SeedAsync(ExerciseContext context, IHostEnvironment env, ILogger<ExerciseContextSeed> logger)
        {
            using (context)
            {
                if (env.IsDevelopment() || env.EnvironmentName == "Test")
                {
                    if (!context.Exercises.Any())
                        context.Exercises.AddRange(GetExercises());

                    await context.SaveChangesAsync();
                }

            };
        }

        private IEnumerable<Exercise> GetExercises()
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
    }
}
